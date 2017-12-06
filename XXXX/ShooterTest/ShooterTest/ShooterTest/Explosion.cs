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



namespace ShooterTest
{
    public class Explosion
    {
        public Texture2D texture;
        //Texture2D empty;
        public Vector2 position;
        public Rectangle bounds;
        public int Health = 50;
        Timer t = new Timer();

        public Explosion(Texture2D texture)
        {
            this.texture = texture;
            t.Interval = 250;
            t.Enabled = true;
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            bounds.X = 800;
            bounds.Y = 800;
        }

        public void Initialize()
        {
            // TODO: Add your initialization code here
        }

        public void Explode(Vector2 position) {
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
