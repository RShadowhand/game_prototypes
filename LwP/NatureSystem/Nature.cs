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

namespace NatureSystem
{
    public class Nature
    {
        GameObjectCollection objects;
        Point boundaries = new Point(0, 250);
        float Speed = 3.0f;
        Random r = new Random();
        SpriteFont font;
        int score = 0;

        Rectangle mRect;
        bool mPress = false;
        GameObject Score;

        public void LoadContent(ContentManager Content)
        {
            objects = ObjectFactory.MakeCollection("Content/CNature", "nature", Content);
            for (int i = 0; i < 20; i++)
            {
                objects.Items.Add(i.ToString(), new GameObject(new Vector2(r.Next(0,400), r.Next(0, 250)), "yagmur", "nature", Content));
            }
            Score = objects.Items["score"];
            objects.Items.Remove("score");
            Score.MoveTo(0, 480 - Score.rTexture.Height);
            font = Content.Load<SpriteFont>("Font1");
            mRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 0, 0); 
        }

        MouseState ms;
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                score = 0;
            }
            ms = Mouse.GetState();
            mRect.Location = new Point(ms.X, ms.Y);
            for (int i = 0; i < 20; i++)
            {
                GameObject rain = objects.Items[i.ToString()];
                Point position = rain.getPosition();
                if (position.X > boundaries.X && position.Y < boundaries.Y)
                {
                    rain.Move(-Speed / 1.5F, Speed / 1.5F);
                }
                else
                {
                    rain.MoveTo(r.Next(0, 800), 0);
                }
            }

            if (ms.LeftButton == ButtonState.Pressed && mPress == false)
            {
                mPress = true;

                for (int i = 0; i < 20; i++)
                {
                    if (mRect.Intersects(objects.Items[i.ToString()].getRect()))
                    {
                        score += 25;
                        objects.Items[i.ToString()].Move(r.Next(0, 400), -objects.Items[i.ToString()].getRect().Location.Y);
                    }
                }

                if (mRect.Intersects(objects.Items["bulut"].getRect()) || mRect.Intersects(objects.Items["bulut1"].getRect()))
                {
                    score += 1;
                }
            }
            if (ms.LeftButton == ButtonState.Released)
            {
                mPress = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            objects.Draw(sb);
            Score.Draw(sb);
            sb.DrawString(font, score.ToString(), new Vector2(90, 400), Color.Black);
        }
    }
}
