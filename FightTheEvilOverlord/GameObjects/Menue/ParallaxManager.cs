﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FightTheEvilOverlord
{
    class ParallaxManager
    {
        public List<ParallaxLayer> Layers { get; private set; }
        public List<ParallaxLayer> HowToLayer { get; private set; }

        int alpha;
        int alphaCha;
        int alphaCha2;
        int menuePosition;
        int ch1Position;
        float ch2Position;
        int iCounter;

        bool hasToFuckUp;
        bool hasToFuckDown;
        bool hasToFuckMenu;

        float scale;

        public ParallaxManager()
        {
            this.scale = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 1920.0f;
            alpha = 0;
            alphaCha = 0;
            alphaCha2 = 0;
            menuePosition = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ch1Position = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ch2Position = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 2;
            this.Layers = new List<ParallaxLayer>();
            this.HowToLayer = new List<ParallaxLayer>();
            hasToFuckUp = false;
            EventManager.OnRender += Draw;
        }

        public void AddLayer(ParallaxLayer layer)
        {
            this.Layers.Add(layer);
        }

        public void AddHowToLayer(ParallaxLayer howTo)
        {
            this.HowToLayer.Add(howTo);
        }

        public void Destroy()
        {
            EventManager.OnRender -= Draw;
            EventManager.OnRender -= DrawTheFuckDown;
            EventManager.OnRender -= DrawTheFuckUp;
        }

        public void Draw(SpriteBatch spriteBach)
        {
            iCounter++;

            if (alpha <= 255 && iCounter >= 300 && !hasToFuckUp)
            {
                alpha++;
            }

            if (menuePosition >= 10 && !hasToFuckUp)
            {
                menuePosition -= 9;
            }

            if (ch1Position >= 10 && !hasToFuckUp)
            {
                ch1Position -= 9;
            }

            if (ch2Position >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - (650 * scale) && !hasToFuckUp)
            {
                ch2Position -= 9;
            }
            foreach (var layer in this.Layers)
            {
                if (alphaCha <= 255 && iCounter >= 200)
                {
                    alphaCha++;
                }

                if (alphaCha2 <= 255 && iCounter >= 270)
                {
                    alphaCha2++;
                }

                if (layer.speed == 3.0f && iCounter >= 300)
                {
                    float multiplierX = Mouse.GetState().Position.X - (layer.Position.X + layer.Image.Width / 2);
                    float multiplierY = Mouse.GetState().Position.Y - (layer.Position.Y + layer.Image.Height / 2);
                    Vector2 newPosition = new Vector2(layer.Position.X + (multiplierX / 200 * layer.speed), layer.Position.Y + (multiplierY / 200 * layer.speed));
                    spriteBach.Draw(layer.Image, newPosition, null, new Color(255, 255, 255, alpha), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else if (layer.speed == 0.0f)
                {
                    spriteBach.Draw(layer.Image, new Vector2(layer.Position.X, 0 - menuePosition), null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else if (layer.speed == 1.0f)
                {
                    spriteBach.Draw(layer.Image, new Vector2(0 - ch1Position + (115*scale), layer.Position.Y + (193*scale)), null, new Color(255, 255, 255, alphaCha), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else if (layer.speed == 2.0f)
                {
                    spriteBach.Draw(layer.Image, new Vector2(0 + ch2Position + (133*scale),layer.Position.Y + (193*scale)), null, new Color(255, 255, 255, alphaCha2), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else
                {
                    spriteBach.Draw(layer.Image, layer.Position, null, new Color(255, 255, 255, 0), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
            }
        }

        public void goUp()
        {
            hasToFuckUp = true;
            hasToFuckDown = false;
            EventManager.OnRender -= Draw;
            if (hasToFuckUp)
            {
                EventManager.OnRender += DrawTheFuckUp;
            }
            else
            {
                EventManager.OnRender -= DrawTheFuckUp;
            }
        }

        public void goDown()
        {
            hasToFuckUp = false;
            hasToFuckDown = true;
            EventManager.OnRender -= Draw;
            if (hasToFuckDown)
            {
                EventManager.OnRender += DrawTheFuckDown;
            }
            else
            {
                EventManager.OnRender -= DrawTheFuckDown;
            }
        }


        public void HowTo()
        {
            hasToFuckUp = false;
            hasToFuckDown = true;
            EventManager.OnRender -= Draw;
            if (hasToFuckDown)
            {
                EventManager.OnRender += DrawHowTo;
            }
            else
            {
                EventManager.OnRender -= DrawHowTo;
                
            }
        }

        public void DrawMenu()
        {
            hasToFuckDown = false;
            hasToFuckUp = false;
            hasToFuckMenu = true;
            EventManager.OnRender -= DrawTheFuckDown;
            EventManager.OnRender -= DrawTheFuckUp;
            EventManager.OnRender -= DrawHowTo;
            if (hasToFuckMenu)
            {
                EventManager.OnRender += Draw;
            }
            else
            {
                EventManager.OnRender -= Draw;
            }
        }

        private void DrawHowTo(SpriteBatch spriteBatch)
        {
           
            foreach (var layer in this.HowToLayer)
            {
                if (layer.speed == 1.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 150)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 2.0f)
                {
                    if (layer.Position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 344.075f)
                    {
                        layer.Position = new Vector2(layer.Position.X - 8, layer.Position.Y);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
              
                else if (layer.speed == 3.0f)
                {
                    if (layer.Position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 220 && layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 150)
                    {
                        layer.Position = new Vector2(layer.Position.X - 8, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 4.0f)
                {
                    if (layer.Position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 220 && layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 150)
                    {
                        layer.Position = new Vector2(layer.Position.X - 8, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 5.0f)
                {
                    if (layer.Position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 220 && layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 150)
                    {
                        layer.Position = new Vector2(layer.Position.X - 8, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 6.0f)
                {
                    if (layer.Position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 220 && layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 150)
                    {
                        layer.Position = new Vector2(layer.Position.X - 8, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 7.0f)
                {
                    if (layer.Position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 220 && layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 150)
                    {
                        layer.Position = new Vector2(layer.Position.X - 8, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else
                {
                    layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 13);
                    spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
            }

        }

        private void DrawTheFuckUp(SpriteBatch spriteBatch)
        {
            foreach (var layer in this.Layers)
            {

                if (layer.speed == 1.0f)
                {
                    spriteBatch.Draw(layer.Image, new Vector2(0 - ch1Position + (115 * scale), layer.Position.Y + (193 * scale)), null, new Color(255, 255, 255, alphaCha), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else if (layer.speed == 2.0f)
                {
                    spriteBatch.Draw(layer.Image, new Vector2(0 + ch2Position + (133 * scale), layer.Position.Y + (193 * scale)), null, new Color(255, 255, 255, alphaCha2), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else if (layer.speed == 94.0f)
                {
                    if (layer.Position.Y <= -10)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 13);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 38.0f)
                {
                    if (layer.Position.Y <= -10)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 13);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 39.0f)
                {
                    if (layer.Position.Y <= -10)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 13);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 40.0f)
                {
                    if (layer.Position.Y <= -10)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 13);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 41.0f)
                {
                    if (layer.Position.Y <= -10)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 13);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 42.0f)
                {
                    if (layer.Position.Y <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 + 100)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 10);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 43.0f)
                {
                    if (layer.Position.Y <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 + 250)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else
                {
                    layer.Position = new Vector2(layer.Position.X, layer.Position.Y + 13);
                    spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }

            }
        }
        private void DrawTheFuckDown(SpriteBatch spriteBatch)
        {
            foreach (var layer in this.Layers)
            {
                if (layer.speed == 1.0f)
                {
                    spriteBatch.Draw(layer.Image, new Vector2(0 - ch1Position + (115*scale), layer.Position.Y + (193*scale)), null, new Color(255, 255, 255, alphaCha), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                else if (layer.speed == 2.0f)
                {
                    spriteBatch.Draw(layer.Image, new Vector2(0 + ch2Position + (133*scale),layer.Position.Y + (193*scale)), null, new Color(255, 255, 255, alphaCha2), 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
                
                else if (layer.speed == 94.0f)
                {
                    if (layer.Position.Y >= -10)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 13);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }

                else if (layer.speed == 39.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 30)
                    {
                        layer.Position = new Vector2(layer.Position.X + 0.07f, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 40.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 + 20)
                    {
                        layer.Position = new Vector2(layer.Position.X + 0.07f, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 41.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 + 65)
                    {
                        layer.Position = new Vector2(layer.Position.X + 0.07f, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 42.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 30)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 43.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 + 20)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else if (layer.speed == 44.0f)
                {
                    if (layer.Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 + 65)
                    {
                        layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 8);
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                    else
                    {
                        spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                    }
                }
                else
                {
                    layer.Position = new Vector2(layer.Position.X, layer.Position.Y - 13);
                    spriteBatch.Draw(layer.Image, layer.Position, null, Color.White, 0.0f, Vector2.Zero, layer.scale, SpriteEffects.None, 1);
                }
            }
        }
    }
}