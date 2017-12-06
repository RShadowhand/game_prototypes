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
    public enum ObjectType
    {
        prop, door, item, container, background, npc
    }

    class Object
    {
        bool isSolid = false;
        public string Name;
        public string textureName;
        public Vector2 Location;
        public int height, width;
        public float size = 2.0f;
        public ObjectType Type;
        public Rectangle rect;

        public Object(string Name, bool isSolid, ObjectType Type, string textureName, Vector2 Location)
        {
            this.Name = Name;
            this.isSolid = isSolid;
            this.textureName = textureName;
            this.Location = Location;
            this.Type = Type;
        }

        public Texture2D currFrame;
        SpriteFont font;
        public void ContentLoad(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("font");
            currFrame = Content.Load<Texture2D>(textureName);
            height = (int)(currFrame.Height * size);
            width = (int)(currFrame.Width * size);
            rect = new Rectangle((int)Location.X, (int)Location.Y, width, height);
        }

        public void Reload() {
            height = (int)(currFrame.Height * size);
            width = (int)(currFrame.Width * size);
            rect = new Rectangle((int)Location.X, (int)Location.Y, width, height);
        }

        //bool isKeyDown = false;

        int elapsed = 0;
        KeyboardState ks;
        public void Update(GameTime gT) 
        {
            elapsed += gT.ElapsedGameTime.Milliseconds;
            ks = Keyboard.GetState();
        }

        public virtual void Draw(SpriteBatch sb) 
        {
            sb.Draw(currFrame, rect, Color.White);
        }
    }
}
