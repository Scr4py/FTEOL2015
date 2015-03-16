using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightTheEvilOverlord
{
    class Background : GameObject
    {
        Transform transform;
        Texture2D image;

        public Background(Texture2D image)
        {
            this.transform = AddComponent<Transform>();
            this.image = image;
            EventManager.OnRender += Render;
        }

        private void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.image, this.transform.Position, Color.White);
        }
    }
}
