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

namespace PC
{
    public class PC
    {
        GameObjectCollection objects;
        DebugHelper debugger;
        GameObject background;
        public void LoadContent(ContentManager Content)
        {
            objects = ObjectFactory.MakeCollection("Content/CPC", "pc", Content);
            background = new GameObject(Vector2.Zero, "arkaplan", "pc", Content);
            debugger = new DebugHelper(objects, Content, true, true);
        }

        public void Update(GameTime gameTime)
        {
            debugger.Update(gameTime, Mouse.GetState());
        }

        public void Draw(SpriteBatch sb)
        {
            background.Draw(sb);
            objects.Draw(sb);
            debugger.Draw(sb);
        }
    }
}
