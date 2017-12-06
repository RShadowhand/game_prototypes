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
            explosion = Content.Load<Texture2D>("creep-1-f2");
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
                            exp = new Explosion(explosion,(new Vector2(Main.enemySpawner.enemies[j].bounds.X, Main.enemySpawner.enemies[j].bounds.Y)));
                            explist.Add(exp);
                            Main.enemySpawner.enemies.RemoveAt(j);
                            Main.Score+=15;

                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < Main.enemySpawner.enemies.Count; i++)
            {
                if (Main.player.bounds.Intersects(Main.enemySpawner.enemies[i].bounds))
                {
                    if (Main.player.invincible == false)
                    {
                        Main.player.Health -= 100;    
                    }
                    exp = new Explosion(explosion, (new Vector2(Main.enemySpawner.enemies[i].bounds.X, Main.enemySpawner.enemies[i].bounds.Y)));
                    explist.Add(exp);
                    Main.enemySpawner.enemies.RemoveAt(i);
                }
            }
            
            for (int i = 0; i < explist.Count; i++)
            {
                explist[i].Update(gameTime);
                if (explist[i].noMoreNeeded == true) 
                {
                    explist.RemoveAt(i);
                }
                
            }
            
        }

        public void Draw(SpriteBatch spritebatch) {
            foreach (Explosion item in explist)
            {
                item.Draw(spritebatch);
            }
        }
    }
}
