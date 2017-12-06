using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace LineTest
{
    public class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle bounds;
        public int Health = 50;

        public Enemy(Texture2D texture, Vector2 position) {
            this.texture = texture;
            this.position = position;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Initialize()
        {
            // TODO: Add your initialization code here
        }

        public void Update(GameTime gameTime)
        {
            bounds.X -= 9;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
