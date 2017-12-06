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
using ObjectHelper;

namespace FragilityGame
{
    public class Fragility
    {
        GameObjectCollection objects, fragiles = new GameObjectCollection();
        List<string> fragileT;
        Random rand = new Random();
        GameObject kid;
        int score;
        SpriteFont font;

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Font1");
            objects = ObjectFactory.MakeCollection("Content/CFragility", "fragility", Content);
            Rectangle r = objects.Items["cimen"].getRect();
            r.Width = 1000;
            objects.Items["cimen"].setRect(r);
            fragileT = TextureLoader.TextureList("Content/fragility/CFragiles");
            
            foreach (string t in fragileT)
            {
                fragiles.AddObject(new GameObject(new Vector2(rand.Next(0,700), rand.Next(0, 150)), t, "fragility", Content));
            }

            kid = new GameObject(new Vector2(0, 480 - 159), "cocuk", "fragility", Content);
        }

        KeyboardState ks;

        public void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape))
            {
                score = 0;
            }
            foreach (GameObject f in fragiles.Items.Values)
            {
                if (f.getRect().Intersects(kid.getRect()))
                {
                    f.MoveTo(rand.Next(0, 700), rand.Next(0-f.getRect().Height, 150));
                    score += 5;
                }
                if (f.getPosition().Y > 480-f.getRect().Height)
                {
                    f.MoveTo(rand.Next(0, 700), rand.Next(0 - f.getRect().Height, 150));
                    score--;
                }
                f.Move(0, 3);
            }
            if (kid.getPosition().X < 800 - kid.getRect().Width && kid.getPosition().X >= 0)
            {
                if (ks.IsKeyDown(Keys.Left))
                {
                    kid.Move(-3, 0);
                }
                if (ks.IsKeyDown(Keys.Right))
                {
                    kid.Move(3, 0);
                }
            }
            if (kid.getPosition().X < 0)
            {
                kid.MoveTo(0, 480 - 159);
            }
            if (kid.getPosition().X > 800 - kid.getRect().Width)
            {
                kid.MoveTo(800 - kid.getRect().Width, 480 - 159);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            objects.Draw(sb);
            fragiles.Draw(sb);
            kid.Draw(sb);
            sb.DrawString(font, score.ToString(), Vector2.Zero, Color.Black);
        }
    }
}
