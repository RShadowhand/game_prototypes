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
    public class Spawner
    {
        public Texture2D texture;
        public List<Enemy> enemies = new List<Enemy>();
        Random r = new Random();
        Enemy baddy;

        public void Initialize(){}

        public void LoadContent(ContentManager Content){
            texture = Content.Load<Texture2D>("baddy");
        }

        public void SpawnEnemy()
        {
            Vector2 rSpot = new Vector2(800 + texture.Width + r.Next(100, 250), r.Next(0, 480 - texture.Height));
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
                enemies[i].position.X -= 6;
                enemies[i].bounds = new Rectangle((int)enemies[i].position.X, (int)enemies[i].position.Y, enemies[i].texture.Width, enemies[i].texture.Height);
                if (enemies[i].bounds.X + enemies[i].texture.Width < 0)
                {
                    enemies.RemoveAt(i);
                }
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
