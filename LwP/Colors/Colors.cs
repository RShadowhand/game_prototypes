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
using System.IO;

namespace ColorsGame
{
    public class Colors
    {
        public Colors()
        {
           
        }

        GameObjectCollection objects;
        Dictionary<string, Song> Sounds = new Dictionary<string, Song>();
        GameObjectCollection buttons = new GameObjectCollection();
        public void LoadContent(ContentManager Content)
        {
            objects = ObjectFactory.MakeCollection("Content/CColors", "colors",Content);
            Sounds.Add("red", Content.Load<Song>("colors/sound/red"));
            Sounds.Add("yellow", Content.Load<Song>("colors/sound/yellow"));
            Sounds.Add("blue", Content.Load<Song>("colors/sound/blue"));
            Sounds.Add("green", Content.Load<Song>("colors/sound/green"));
            Sounds.Add("orange", Content.Load<Song>("colors/sound/orange"));
            Sounds.Add("purple", Content.Load<Song>("colors/sound/purple"));

            buttons.AddObject(objects.Items["k_kulaklik"]);
            buttons.AddObject(objects.Items["m_kulaklik"]);
            buttons.AddObject(objects.Items["mr_kulaklik"]);
            buttons.AddObject(objects.Items["s_kulaklik"]);
            buttons.AddObject(objects.Items["y_kulaklik"]);
            buttons.AddObject(objects.Items["t_kulaklik"]);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mS = Mouse.GetState();
            Rectangle mRect = new Rectangle(mS.X, mS.Y, 1, 1);
            string soundName = "";
            foreach (string key in buttons.Items.Keys)
            {
                if (mRect.Intersects(buttons.Items[key].getRect()) && mS.LeftButton == ButtonState.Pressed)
                {
                    if (buttons.Items[key].firsttexture.Split(new char[] { '_' })[0] == "m")
                    {
                        soundName = "blue";
                    }
                    else if (buttons.Items[key].firsttexture.Split(new char[] { '_' })[0] == "mr")
                    {
                        soundName = "purple";
                    }
                    else if (buttons.Items[key].firsttexture.Split(new char[] { '_' })[0] == "k")
                    {
                        soundName = "red";
                    }
                    else if (buttons.Items[key].firsttexture.Split(new char[] { '_' })[0] == "s")
                    {
                        soundName = "yellow";
                    }
                    else if (buttons.Items[key].firsttexture.Split(new char[] { '_' })[0] == "t")
                    {
                        soundName = "orange";
                    }
                    else if (buttons.Items[key].firsttexture.Split(new char[] { '_' })[0] == "y")
                    {
                        soundName = "green";
                    }
                    MediaPlayer.Play(Sounds[soundName]);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            objects.Draw(sb);
        }
    }
}
