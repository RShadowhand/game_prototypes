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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>

    public class Bullet
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle bounds;

        public Bullet(Texture2D texture, Vector2 position) {
            this.texture = texture;
            this.position = position;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Initialize(){}

        public void Update(GameTime gameTime){}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.bounds, Color.White);
        }
    }
}
