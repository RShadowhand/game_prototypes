using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace LineTest
{
    public class Pipes
    {
        public Texture2D pipeup, pipedown;
        public Rectangle bUp, bDown;
        public int Health = 50;

        public Pipes(ContentManager Content, Vector2 position) {
            this.pipeup = Content.Load<Texture2D>("pipeup");
            this.pipedown = Content.Load<Texture2D>("pipedown");
            this.bDown = new Rectangle(290, (int)position.Y - pipedown.Height, pipedown.Width, pipedown.Height);
            int upPos = (int)position.Y + 100;
            this.bUp = new Rectangle(290, upPos, pipeup.Width, pipeup.Height);
        }

        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            bUp.X -= 3;
            bDown.X -= 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pipeup, bUp, Color.White);
            spriteBatch.Draw(pipedown, bDown, Color.White);
        }
    }
}
