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
    public class Spawner
    {
        public Texture2D texture;
        public List<Pipes> enemies = new List<Pipes>();
        Random r = new Random();
        Pipes pipes;
        GraphicsDeviceManager graphics;
        ContentManager Content;
        public int Score = 0;

        public Spawner(GraphicsDeviceManager graphics, ContentManager Content) 
        {
            this.graphics = graphics;
            this.Content = Content;
            SpawnEnemy();
        }

        public void Initialize(){
            SpawnEnemy();
        }

        public void LoadContent(ContentManager Content){
            
        }

        public void SpawnEnemy()
        {
            float Spot = r.Next(100, 250);
            pipes = new Pipes(Content, new Vector2(0, Spot));
            enemies.Add(pipes);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies.Count < 2)
                {
                    if (enemies[i].bUp.X + enemies[i].pipeup.Width < 120)
                    {
                        SpawnEnemy();
                        Score++;
                    }
                }
                if (enemies[i].bUp.X + enemies[i].pipeup.Width < 0)
                {
                    enemies.RemoveAt(i);
                }
                enemies[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in enemies)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
