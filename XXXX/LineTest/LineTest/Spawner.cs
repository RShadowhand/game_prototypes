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
    public class Spawner
    {
        public Texture2D texture;
        public List<Enemy> enemies = new List<Enemy>();
        Random r = new Random();
        Enemy baddy;
        GraphicsDeviceManager graphics;

        public Spawner(GraphicsDeviceManager graphics) 
        {
            this.graphics = graphics;
        }

        public void Initialize(){}

        public void LoadContent(ContentManager Content){
            texture = Content.Load<Texture2D>("creep-1");
        }

        public void SpawnEnemy()
        {
            Vector2 rSpot = new Vector2(1280 + texture.Width + r.Next(100, 900), r.Next(0, 480 - texture.Height));
            baddy = new Enemy(texture, rSpot);
            enemies.Add(baddy);
        }

        public void Update(GameTime gameTime)
        {
            if (enemies.Count < 10)
            {
                SpawnEnemy();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].bounds.X + enemies[i].texture.Width < 0)
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
