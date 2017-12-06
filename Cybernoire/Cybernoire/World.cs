using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Cybernoire
{
    class World
    {
        Object background = new Object("bg", false, ObjectType.background, "", Vector2.Zero);

        int elapsed = 0;
        public int height = 0, position = 0;
        Color backgroundColor;
        SpriteFont font;
        string Name;

        public World(string Name, GraphicsDevice gr, Color c, SpriteFont font)
        {
            this.Name = Name;
            this.font = font;
            backgroundColor = c;
            background.currFrame = new Texture2D(gr, 800, 600, false, SurfaceFormat.Color);
            Color[] filler = new Color[800 * 600];
            for (int i = 0; i < 800 * 600; i++)
            {
                filler[i] = backgroundColor;
            }
            background.currFrame.SetData(filler);
            background.Reload();
            addObject(background);
        }

        public Dictionary<string, Object> WorldObjects = new Dictionary<string, Object>();

        public void ContentLoad(ContentManager Content) 
        {
            foreach (Object o in WorldObjects.Values)
            {
                if (o.Type != ObjectType.background)
                {
                    o.ContentLoad(Content);
                }
            }
        }

        public void addObject(Object o) {
            WorldObjects.Add(o.Name, o);
        }

        public void remObject(Object o)
        {
            WorldObjects.Remove(o.Name);
        }

        public void Update(GameTime gT) 
        {
            elapsed += gT.ElapsedGameTime.Milliseconds;
            foreach (string k in WorldObjects.Keys)
            {
                WorldObjects[k].Update(gT);
            }
        }

        public void Draw(SpriteBatch sb) 
        {
            foreach (string k in WorldObjects.Keys)
            {
                WorldObjects[k].Draw(sb);
            }
            sb.DrawString(font, "Location: "+Name, Vector2.Zero, Color.White);
        }
    }
}
