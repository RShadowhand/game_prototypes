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

namespace ObjectHelper
{
    public class GameObjectCollection
    {

        public Dictionary<string, GameObject> Items = new Dictionary<string, GameObject>();

        public GameObjectCollection()
        {

        }

        public void CreateGameObject(Vector2 pos, string texture, string texturepath, string name, ContentManager Content) 
        {
            
        }

        public void AddObject(GameObject GameObject) { Items.Add(GameObject.firsttexture, GameObject); }

        public void Draw(SpriteBatch sb)
        {
            foreach (string key in Items.Keys)
            {
                Items[key].Draw(sb);
            }
        }
    }
}
