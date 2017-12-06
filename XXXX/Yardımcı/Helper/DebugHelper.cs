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

namespace ObjectHelper
{
    public class DebugHelper
    {
        GameObjectCollection col;
        SpriteFont font;
        public string target = "";
        public string text = "";
        Rectangle mRect;
        bool FullDebug = false;
        bool clicked = false;
        bool texton = false;

        public DebugHelper(GameObjectCollection col, ContentManager Content, bool FullDebug, bool texton) 
        {
            this.col = col;
            this.FullDebug = FullDebug;
            this.texton = texton;
            font = Content.Load<SpriteFont>("Font1");
            mRect = new Rectangle(0, 0, 1, 1);
        }

        public void Update(GameTime gameTime, MouseState mState)
        {
            mRect.Location = new Point(mState.X, mState.Y);
            


            foreach (string key in col.Items.Keys)
            {
                if (mRect.Intersects(col.Items[key].getRect()))
                {
                    target = key;
                    if (FullDebug)
                    {
                        text = key + " - " + col.Items[key].getPosition().ToString() + " - x:" + col.Items[key].getRect().Height + " y:" + col.Items[key].getRect().Width;
                    }
                    else
                    {
                        text = key;
                    }
                    
                }
            }

            if (FullDebug)
            {
                foreach (string key in col.Items.Keys)
                {
                    if (mRect.Intersects(col.Items[key].getRect()) && mState.LeftButton == ButtonState.Pressed && clicked == false)
                    {
                        if (col.Items[key].Selected)
                        {
                            col.Items[key].Deselect();
                        }
                        else
                        {
                            col.Items[key].Select();
                        }
                    }
                }
                foreach (string key in col.Items.Keys)
                {
                    if (col.Items[key].Selected)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            col.Items[key].Move(-1, 0);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            col.Items[key].Move(1, 0);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            col.Items[key].Move(0, -1);
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            col.Items[key].Move(0, 1);
                        }
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

        public void Draw(SpriteBatch sb) 
        {
            if (texton)
            {
                sb.DrawString(font, text, Vector2.Zero, Color.Black);
            }
            if (target != "")
            {
                if (col.Items[target].Selected)
                {
                    sb.Draw(col.Items[target].rTexture, col.Items[target].getRect(), Color.Black);
                }
                else if(FullDebug == true)
                {
                    sb.Draw(col.Items[target].rTexture, col.Items[target].getRect(), Color.Red);
                }
                else
                {
                    sb.Draw(col.Items[target].rTexture, col.Items[target].getRect(), Color.Blue);
                }
            }
        }
    }
}
