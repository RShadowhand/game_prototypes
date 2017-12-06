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

namespace LettersGame
{
    public class Letters
    {
        public Letters()
        {
           
        }

        GameObjectCollection objects;
        List<string> LetterList = new List<string>();
        Dictionary<string, Song> Sounds = new Dictionary<string, Song>();

        public void LoadContent(ContentManager Content)
        {
            objects = ObjectFactory.MakeCollection("Content/CLetters", "letters/images", Content);
            foreach (string key in objects.Items.Keys)
            {
                LetterList.Add(key);
                Sounds.Add(key, Content.Load<Song>("letters/sounds/"+key));
            }
        }

        public void Update(GameTime gameTime)
        {
            MouseState mS = Mouse.GetState();
            Rectangle mRect = new Rectangle(mS.X, mS.Y, 1, 1);
            foreach (string ses in LetterList)
            {
                if (objects.Items[ses].getRect().Intersects(mRect) && mS.LeftButton == ButtonState.Pressed)
                {
                    MediaPlayer.Play(Sounds[ses]);
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
