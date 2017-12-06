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


namespace MapGame
{
    public class Map
    {
        GameObjectCollection objects;
        DebugHelper debugger;
        SpriteFont font;

        string Query = "";
        string target = "";
        string aimed = "";
        int Score = 0;
        List<string> Liste = new List<string>();
        Random r = new Random();
        bool clicked = false;


        public void LoadContent(ContentManager Content)
        {
            objects = ObjectFactory.MakeCollection("Content/CIller", "iller", Content);
            debugger = new DebugHelper(objects, Content, false, false);
            font = Content.Load<SpriteFont>("Font1");
            foreach (string key in objects.Items.Keys)
            {
                Liste.Add(key.Split(new char[] { '-' })[1].ToUpperInvariant());
            }
            NewCity();
        }

        MouseState mState;
        Rectangle mRect = new Rectangle(0, 0, 1, 1);
        public void Update(GameTime gameTime)
        {
            mState = Mouse.GetState();
            mRect.Location = new Point(mState.X, mState.Y);
            debugger.Update(gameTime, Mouse.GetState());
            if (debugger.target != "")
            {
                aimed = debugger.target.Split(new char[] { '-' })[1].ToUpperInvariant();
            }

            foreach (GameObject g in objects.Items.Values)
            {
                if (mRect.Intersects(g.getRect()) && mState.LeftButton == ButtonState.Pressed && clicked == false)
                {
                    if (aimed == target)
                    {
                        Score += 10;
                        NewCity();
                    }
                }
            }

            if (mState.LeftButton == ButtonState.Pressed)
            {
                clicked = true;
            }
            else if (mState.LeftButton == ButtonState.Released)
            {
                clicked = false;
            }
        }

        public void NewCity() {
            target = Liste[r.Next(0, Liste.Count)];
            Query = "Find " + target + " on the map!";
        }

        public void Draw(SpriteBatch sb)
        {
            objects.Draw(sb);
            debugger.Draw(sb);
            sb.DrawString(font, Query, Vector2.Zero, Color.Black);
            sb.DrawString(font, Score.ToString(), new Vector2(0, 460), Color.Black);
        }
    }
}
