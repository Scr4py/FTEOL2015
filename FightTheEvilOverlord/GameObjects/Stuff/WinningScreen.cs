﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightTheEvilOverlord
{
    class WinningScreen : GameObject
    {
        public static event Action GameFinished;

        Renderer render;
        Transform trans;
        float timer;
        float delay = 5000;

        public WinningScreen(Texture2D tex)
        {
            trans = AddComponent<Transform>();
            render = AddComponent<Renderer>();
            render.Start();
            render.SetImage(tex);
            render.SecScale = 1;
            EventManager.OnUpdate += Update;
            DeactivateKI();
            GameFinished();
            Utility.map.Destroy();
        }

        private void DeactivateKI()
        {
            if (Utility.ArchPlayer.KIControlled)
            {
                Utility.ArchPlayer.KIControlled = false;
            }
            else if (Utility.PigPlayer.KIControlled)
            {
                Utility.PigPlayer.KIControlled = false;
            }
            else if (Utility.SwordPlayer.KIControlled)
            {
                Utility.SwordPlayer.KIControlled = false;
            }
            else if (Utility.EvilOverLord.KIControlled)
            {
                Utility.EvilOverLord.KIControlled = false;
            }
        }

        private void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer > delay)
            {
                Utility.MenuActive = true;
                render.Destroy();
                this.Destroy();
            }
        }
    }
}
