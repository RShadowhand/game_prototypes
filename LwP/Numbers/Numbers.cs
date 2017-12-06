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

namespace NumbersGame
{
    public class Numbers
    {
        GameObjectCollection objects;
        List<Song> sesler = new List<Song>();
        public void LoadContent(ContentManager Content)
        {
            objects = ObjectFactory.MakeCollection("Content/CNumbers", "numbers/images", Content);
            for (int i = 0; i <= 9; i++)
            {
                sesler.Add(Content.Load<Song>("numbers/sound/"+i.ToString()));
            }
        }

        public void Update(GameTime gameTime)
        {
            MouseState mS = Mouse.GetState();
            Rectangle mRect = new Rectangle(mS.X, mS.Y, 1,1);
            for (int i = 0; i <= 9; i++)
            {
                if (objects.Items[i.ToString()].getRect().Intersects(mRect) && mS.LeftButton == ButtonState.Pressed)
                {
                    MediaPlayer.Play(sesler[i]);
                    break;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            objects.Draw(sb);
        }
    }
}
