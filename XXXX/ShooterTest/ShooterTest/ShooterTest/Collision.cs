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


namespace ShooterTest
{
    public class CollisionEngine
    {
        Game1 Main = null;
        List<Explosion> explist = new List<Explosion>();
        Explosion exp;
        Texture2D explosion;

        public CollisionEngine(Game callingForm) {
            Main = callingForm as Game1;
        }

        public void Initialize(){}

        public void LoadContent(ContentManager Content) {
            explosion = Content.Load<Texture2D>("explosion");
            exp = new Explosion(explosion);
        }

        public void Update(GameTime gameTime)
        {

            if (Main.player.bullets.Count > 0)
            {
                for (int j = 0; j < Main.enemySpawner.enemies.Count; j++)
                {
                    for (int i = 0; i < Main.player.bullets.Count; i++)
                    {
                        if (Main.player.bullets[i].bounds.Intersects(Main.enemySpawner.enemies[j].bounds))
                        {
                            Main.player.bullets.RemoveAt(i);
                            exp.Explode(Main.enemySpawner.enemies[j].position);
                            Main.enemySpawner.enemies.RemoveAt(j);
                            Main.Score++;
                            
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < Main.enemySpawner.enemies.Count; i++)
            {
                if (Main.player.bounds.Intersects(Main.enemySpawner.enemies[i].bounds))
                {
                    Main.player.Health -= 10;
                    exp.Explode(Main.enemySpawner.enemies[i].position);
                    Main.enemySpawner.enemies.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spritebatch) {
            exp.Draw(spritebatch);
        }
    }
}
