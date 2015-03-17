﻿using System;
using System.Collections.Generic;

namespace FightTheEvilOverlord
{
    class KITrigger : Component
    {
        public Tile toMoveToTile;
        public int activeTiles;
        FightManager fightManager;
        float timer;
        public float delay = 1000;

        Tile nextTile;

        public void Start()
        {
            this.fightManager = GameObject.GetComponent<FightManager>();
            EventManager.OnUpdate += Update;
        }

        private void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
        }

        public void checkNextTiles(Map map)
        {
            activeTiles = 0;

            foreach (Tile tile in map.tilesArray)
            {
                if (tile != null)
                {
                    if (tile.isActive)
                    {
                        activeTiles++;

                        if (timer > delay)
                        {
                            timer = 0;
                            checkBaseTile(tile);

                            if (tile.isActive)
                            {
                                checkFirstCircleNextTiles(tile);

                                if (tile.isActive)
                                {
                                    checkSecondCircleNextTiles(tile);

                                    if (tile.isActive)
                                    {
                                        checkThirdCicleNextTiles(tile);

                                        if (tile.isActive)
                                        {
                                            checkFourthCircleNextTiles(tile);

                                            if (tile.isActive)
                                            {
                                                collectUnits(tile);

                                                if (tile.isActive)
                                                {
                                                    baseMove(tile, map);

                                                    if (tile.isActive)
                                                    {
                                                        tile.isActive = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        void checkBaseTile(Tile baseTile)
        {
            foreach (Tile nextTile in baseTile.nextTiles)
            {
                if (((nextTile.owner == 0 || nextTile.owner == 1 || nextTile.owner == 2) && baseTile.owner == 3) || (nextTile.owner == 3 && (baseTile.owner == 0 || baseTile.owner == 1 || baseTile.owner == 2)))
                {
                    if (checkForStraightAttackableEnemys(baseTile, nextTile))
                    {
                        fightManager.Attack(baseTile, nextTile);
                        baseTile.isActive = false;
                    }
                }
            }

            if (baseTile.nextVillages.Count == 1 && baseTile.isActive == true)
            {
                if (Utility.ActivePlayerNumber == 0 || Utility.ActivePlayerNumber == 1 || Utility.ActivePlayerNumber == 2)
                {
                    if (baseTile.nextVillages[0].owner == 3 || baseTile.nextVillages[0].owner == 4)
                    {
                        if (baseTile.archer != null)
                        {
                            baseTile.archer.activeSoldiers = 0;
                            baseTile.nextVillages[0].owner = Utility.ActivePlayerNumber;
                            baseTile.isActive = false;
                        }
                        else if (baseTile.pigs != null)
                        {
                            baseTile.pigs.activeSoldiers = 0;
                            baseTile.nextVillages[0].owner = Utility.ActivePlayerNumber;
                            baseTile.isActive = false;
                        }
                        else if (baseTile.swords != null)
                        {
                            baseTile.swords.activeSoldiers = 0;
                            baseTile.nextVillages[0].owner = Utility.ActivePlayerNumber;
                            baseTile.isActive = false;
                        }
                    }
                }

                else if (Utility.ActivePlayerNumber == 3)
                {
                    if (baseTile.nextVillages[0].owner != 3)
                    {
                        if (baseTile.archer != null)
                        {
                            baseTile.archer.activeSoldiers = 0;
                            baseTile.nextVillages[0].owner = 3;
                            baseTile.nextVillages[0].conquerUnit = 0;
                            baseTile.isActive = false;
                        }
                        else if (baseTile.pigs != null)
                        {
                            baseTile.pigs.activeSoldiers = 0;
                            baseTile.nextVillages[0].owner = 3;
                            baseTile.nextVillages[0].conquerUnit = 1;
                            baseTile.isActive = false;
                        }
                        else if (baseTile.swords != null)
                        {
                            baseTile.swords.activeSoldiers = 0;
                            baseTile.nextVillages[0].owner = 3;
                            baseTile.nextVillages[0].conquerUnit = 2;
                            baseTile.isActive = false;
                        }
                    }
                }
            }
        }

        void checkFirstCircleNextTiles(Tile baseTile)
        {
            foreach (Tile toRateTile in baseTile.nextTiles)
            {
                if (((toRateTile.owner == 0 || toRateTile.owner == 1 || toRateTile.owner == 2) && baseTile.owner == 3) || (toRateTile.owner == 3 && (baseTile.owner == 0 || baseTile.owner == 1 || baseTile.owner == 2)))
                {
                    if (checkForAttackableEnemys(baseTile, toRateTile))
                    {
                        MoveSoldiers(baseTile, toRateTile);
                    }
                }
                else if (toRateTile.archer == null && toRateTile.pigs == null && toRateTile.swords == null)
                {
                    CheckTile(toRateTile, baseTile, toRateTile);
                }
            }
        }

        void checkSecondCircleNextTiles(Tile baseTile)
        {
            foreach (Tile nextTile in baseTile.nextTiles)
            {
                foreach (Tile toRateTile in nextTile.nextTiles)
                {
                    if (toRateTile != baseTile)
                    {
                        if (((toRateTile.owner == 0 || toRateTile.owner == 1 || toRateTile.owner == 2) && baseTile.owner == 3) || (toRateTile.owner == 3 && (baseTile.owner == 0 || baseTile.owner == 1 || baseTile.owner == 2)))
                        {
                            if (checkForAttackableEnemys(baseTile, toRateTile))
                            {
                                MoveSoldiers(baseTile, nextTile);
                            }
                        }
                        else if (nextTile.archer == null && nextTile.pigs == null && nextTile.swords == null)
                        {
                            CheckTile(toRateTile, baseTile, nextTile);
                        }
                    }
                }
            }
        }

        void checkThirdCicleNextTiles(Tile baseTile)
        {
            foreach (Tile nextTile in baseTile.nextTiles)
            {
                foreach (Tile  remoteTile in nextTile.nextTiles)
                {
                    if (remoteTile != baseTile)
                    {
                        foreach (Tile toRateTile in remoteTile.nextTiles)
                        {
                            if (!nextTile.nextTiles.Contains(toRateTile))
                            {
                                if (((toRateTile.owner == 0 || toRateTile.owner == 1 || toRateTile.owner == 2) && baseTile.owner == 3) || (toRateTile.owner == 3 && (baseTile.owner == 0 || baseTile.owner == 1 || baseTile.owner == 2)))
                                {
                                    if (checkForAttackableEnemys(baseTile, toRateTile))
                                    {
                                        MoveSoldiers(baseTile, nextTile);
                                    }
                                }
                                else if (nextTile.archer == null && nextTile.pigs == null && nextTile.swords == null)
                                {
                                    CheckTile(toRateTile, baseTile, nextTile);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void checkFourthCircleNextTiles(Tile baseTile)
        {
            foreach (Tile nextTile in baseTile.nextTiles)
            {
                foreach (Tile remoteTile in nextTile.nextTiles)
                {
                    if (remoteTile != baseTile)
                    {
                        foreach (Tile moreRemoteTile in remoteTile.nextTiles)
                        {
                            if (!remoteTile.nextTiles.Contains(moreRemoteTile))
                            {
                                foreach (Tile toRateTile in moreRemoteTile.nextTiles)
                                {
                                    if (!moreRemoteTile.nextTiles.Contains(toRateTile))
                                    {
                                        if (((toRateTile.owner == 0 || toRateTile.owner == 1 || toRateTile.owner == 2) && baseTile.owner == 3) || (toRateTile.owner == 3 && (baseTile.owner == 0 || baseTile.owner == 1 || baseTile.owner == 2)))
                                        {
                                            if (checkForAttackableEnemys(baseTile, toRateTile))
                                            {
                                                MoveSoldiers(baseTile, nextTile);
                                            }
                                        }
                                        else if (nextTile.archer == null && nextTile.pigs == null && nextTile.swords == null)
                                        {
                                            CheckTile(toRateTile, baseTile, nextTile);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void collectUnits(Tile currentTile)
        {
            foreach (var nextTile in currentTile.nextTiles)
            {
                if (currentTile.archer != null)
                {
                    if (nextTile.archer != null)
                    {
                        MoveSoldiers(currentTile, nextTile);
                        nextTile.isActive = false;
                    }
                }
                else if (currentTile.pigs != null)
                {
                    if (nextTile.pigs != null)
                    {
                        MoveSoldiers(currentTile, nextTile);
                        nextTile.isActive = false;
                    }
                }
                else if (currentTile.swords != null)
                {
                    if (nextTile.swords != null)
                    {
                        MoveSoldiers(currentTile, nextTile);
                        nextTile.isActive = false;
                    }
                }
            }
        }

        void baseMove(Tile currentTile, Map map)
        {
            if (currentTile.mapX % 2 == 0)
            {
                if (Utility.ActivePlayerNumber == 0 || Utility.ActivePlayerNumber == 1 || Utility.ActivePlayerNumber == 2)
                {
                    if (currentTile.mapX < map.mapWidth)
                    {
                        if (map.tilesArray[currentTile.mapX + 1, currentTile.mapY] != null)
                        {
                            MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX + 1, currentTile.mapY]);
                        }
                        else if (currentTile.mapY < map.mapHeight)
                        {
                            if (map.tilesArray[currentTile.mapX + 1, currentTile.mapY + 1] != null)
                            {
                                MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX + 1, currentTile.mapY - 1]);
                            }
                        }
                    }
                }
                else if (Utility.ActivePlayerNumber == 3)
                {
                    if (currentTile.mapX > 0)
                    {
                        if (map.tilesArray[currentTile.mapX - 1, currentTile.mapY] != null)
                        {
                            MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX - 1, currentTile.mapY]);
                        }
                        else if (currentTile.mapY < map.mapHeight)
                        {
                            if (map.tilesArray[currentTile.mapX - 1, currentTile.mapY + 1] != null)
                            {
                                MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX - 1, currentTile.mapY - 1]);
                            }
                        }
                    }
                }

            }
            else if (currentTile.mapX % 2 != 0)
            {
                if (Utility.ActivePlayerNumber == 0 || Utility.ActivePlayerNumber == 1 || Utility.ActivePlayerNumber == 2)
                {
                    if (currentTile.mapX < map.mapWidth - 1)
                    {
                        if (map.tilesArray[currentTile.mapX + 1, currentTile.mapY] != null)
                        {
                            MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX + 1, currentTile.mapY]);
                        }
                        else if (currentTile.mapY > 0)
                        {
                            if (map.tilesArray[currentTile.mapX + 1, currentTile.mapY - 1] != null)
                            {
                                MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX + 1, currentTile.mapY + 1]);
                            }
                        }
                    }
                }
                else if (Utility.ActivePlayerNumber == 3)
                {
                    if (currentTile.mapX > 0)
                    {
                        if (map.tilesArray[currentTile.mapX - 1, currentTile.mapY] != null)
                        {
                            MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX - 1, currentTile.mapY]);
                        }
                        else if (currentTile.mapY > 0)
                        {
                            if (map.tilesArray[currentTile.mapX - 1, currentTile.mapY - 1] != null)
                            {
                                MoveSoldiers(currentTile, map.tilesArray[currentTile.mapX - 1, currentTile.mapY + 1]);
                            }
                        }
                    }
                }
            }
        }

        void CheckTile(Tile toRateTile, Tile baseTile, Tile toMoveOnTile)
        {
            if (toRateTile.nextVillages.Count > 0)
            {
                if (((toRateTile.nextVillages[0].owner == 0 || toRateTile.nextVillages[0].owner == 1 || toRateTile.nextVillages[0].owner == 2) && baseTile.owner == 3) || (toRateTile.nextVillages[0].owner == 3 && (baseTile.owner == 0 || baseTile.owner == 1 || baseTile.owner == 2)))
                {
                    MoveSoldiers(baseTile, toMoveOnTile);
                    baseTile.isActive = false;
                }
            }
        }

        void MoveSoldiers(Tile currentTile, Tile nextTile)
        {
            if (currentTile.archer != null)
            {
                if (nextTile.archer == null  && nextTile.pigs == null && nextTile.swords == null)
                {
                    nextTile.archer = new Archer(nextTile, Utility.ActivePlayerNumber, 0, currentTile.archer.activeSoldiers, currentTile.archer.image, Utility.EvilOverLord);
                    currentTile.archer.totalSoldiers -= currentTile.archer.activeSoldiers;
                    nextTile.owner = Utility.ActivePlayerNumber;
                    currentTile.isActive = false;

                        currentTile.owner = 4;
                        currentTile.archer.Destroy();
                        currentTile.archer = null;
                }
                else if (nextTile.archer != null)
                {
                    if (nextTile.archer.playerNumber == Utility.ActivePlayerNumber)
                    {
                        nextTile.archer.totalSoldiers += currentTile.archer.activeSoldiers;
                        currentTile.archer.totalSoldiers -= currentTile.archer.activeSoldiers;
                        currentTile.isActive = false;

                            currentTile.owner = 4;
                            currentTile.archer.Destroy();
                            currentTile.archer = null;
                    }
                }
            }

            else if (currentTile.pigs != null)
            {
                if (nextTile.archer == null && nextTile.pigs == null && nextTile.swords == null)
                {
                    nextTile.pigs = new FlyingPigs(nextTile, Utility.ActivePlayerNumber, 0, currentTile.pigs.activeSoldiers, currentTile.pigs.image, Utility.EvilOverLord);
                    currentTile.pigs.totalSoldiers -= currentTile.pigs.activeSoldiers;
                    nextTile.owner = Utility.ActivePlayerNumber;
                    currentTile.isActive = false;


                        currentTile.owner = 4;
                        currentTile.pigs.Destroy();
                        currentTile.pigs = null;
                }
                else if (nextTile.pigs != null)
                {
                    if (nextTile.pigs.playerNumber == Utility.ActivePlayerNumber)
                    {
                        nextTile.pigs.totalSoldiers += currentTile.pigs.activeSoldiers;
                        currentTile.pigs.totalSoldiers -= currentTile.pigs.activeSoldiers;
                        currentTile.isActive = false;

                            currentTile.owner = 4;
                            currentTile.pigs.Destroy();
                            currentTile.pigs = null;
                    }
                }
            }

            else if (currentTile.swords != null)
            {
                if (nextTile.archer == null && nextTile.pigs == null && nextTile.swords ==  null)
                {
                    nextTile.swords = new SwordsMen(nextTile, Utility.ActivePlayerNumber, 0, currentTile.swords.activeSoldiers, currentTile.swords.image, Utility.EvilOverLord);
                    currentTile.swords.totalSoldiers -= currentTile.swords.activeSoldiers;
                    nextTile.owner = Utility.ActivePlayerNumber;
                    currentTile.isActive = false;


                        currentTile.owner = 4;
                        currentTile.swords.Destroy();
                        currentTile.swords = null;
                }
                else if (nextTile.swords != null)
                {
                    if (nextTile.swords.playerNumber == Utility.ActivePlayerNumber)
                    {
                        nextTile.swords.totalSoldiers += currentTile.swords.activeSoldiers;
                        currentTile.swords.totalSoldiers -= currentTile.swords.activeSoldiers;
                        currentTile.isActive = false;

                            currentTile.owner = 4;
                            currentTile.swords.Destroy();
                            currentTile.swords = null;
                    }
                }
            }
        }

        bool checkForStraightAttackableEnemys(Tile currentTile, Tile toRateTile)
        {
            if (currentTile.archer != null)
            {
                if (toRateTile.archer != null)
                {
                    return true;
                }
                else if (toRateTile.pigs != null)
                {
                    return true;
                }
                else if (toRateTile.swords != null)
                {
                    if (toRateTile.Type == "wald")
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (currentTile.pigs != null)
            {
                if (toRateTile.archer != null)
                {
                    if (toRateTile.Type == "berg")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (toRateTile.pigs != null)
                {
                    return true;
                }
                else if (toRateTile.swords != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (currentTile.swords != null)
            {
                if (toRateTile.archer != null)
                {
                    return true;
                }
                else if (toRateTile.pigs != null)
                {
                    if (toRateTile.Type == "feld")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (toRateTile.swords != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        bool checkForAttackableEnemys(Tile currentTile, Tile toRateTile)
        {
            if (currentTile.archer != null)
            {
                if (toRateTile.archer != null)
                {
                    return true;
                }
                else if (toRateTile.pigs != null)
                {
                    return true;
                }
                else if (toRateTile.swords != null)
                {
                    if (toRateTile.Type == "wald")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (currentTile.pigs != null)
            {
                if (toRateTile.archer != null)
                {
                    if (toRateTile.Type == "berg")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (toRateTile.pigs != null)
                {
                    return true;
                }
                else if (toRateTile.swords != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (currentTile.swords != null)
            {
                if (toRateTile.archer != null)
                {
                    return true;
                }
                else if (toRateTile.pigs != null)
                {
                    if (toRateTile.Type == "feld")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (toRateTile.swords != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= Update;
            base.Destroy();
        }
    }
}
