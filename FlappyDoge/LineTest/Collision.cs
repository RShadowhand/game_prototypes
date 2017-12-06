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
    public class CollisionEngine
    {
        Game1 Main = null;

        public CollisionEngine(Game callingForm) {
            Main = callingForm as Game1;
        }

        public void Initialize(){}

        public void LoadContent(ContentManager Content) {
            
        }

        public void Update(GameTime gameTime)
        {
            foreach (Pipes p in Main.enemySpawner.enemies)
            {
                if (Main.player.bounds.Intersects(p.bUp) || Main.player.bounds.Intersects(p.bDown))
                {
                    Main.Score = Main.enemySpawner.Score;
                    Main.gs = GameState.dead;
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch) {

        }
    }
}
