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
using System.Timers;



namespace LineTest
{
    public class Explosion
    {
        public Texture2D texture;
        //Texture2D empty;
        
        public Rectangle bounds;
        public int Health = 50;
        Timer t = new Timer();
        public bool noMoreNeeded = false;

        public Explosion(Texture2D texture, Vector2 position)
        {
            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
            this.texture = texture;
            t.Interval = 500;
            t.Enabled = true;
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            noMoreNeeded = true;
        }

        public void Initialize()
        {
            // TODO: Add your initialization code here
        }

        public void Update(GameTime gameTime)
        {
            bounds.X -= 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
