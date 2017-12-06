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
    class Door : Object
    {
        bool isSolid = false;
        World world;
        public string connectsTo;

        public Door(String Name, World world, string connectsTo, bool isSolid, string textureName, Vector2 Location)
            : base(Name, isSolid, ObjectType.door, textureName, Location)
        {
            this.world = world;
            this.connectsTo = connectsTo;
        }

        public World Use(string door)
        {
            world.position = world.WorldObjects[connectsTo].rect.X;
            return world;
        }
    }

    class NPC : Object
    {
        bool isSolid = false;
        List<Item> inventory = new List<Item>();
        string text = "asdasd";
        string afterText = "asdaxd";
        SpriteFont font;

        public NPC(String Name, bool isSolid, string textureName, Vector2 Location, SpriteFont font, string text, string afterText)
            : base(Name, isSolid, ObjectType.npc, textureName, Location)
        {
            this.font = font;
            this.text = text;
            this.afterText = afterText;
        }

        public void Use(Item i)
        {
            inventory.Add(i);
            text = afterText;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, text, new Vector2(rect.X-60, rect.Y-30), Color.White);
            base.Draw(sb);
        }
    }

    class Container : Object
    {
        List<Item> inventory = new List<Item>();

        public Container(String Name, Item i, bool isSolid, string textureName, Vector2 Location)
            : base(Name, isSolid, ObjectType.container, textureName, Location)
        {
            inventory.Add(i);
        }

        public Item Use()
        {
            Item i;
            if (inventory.Count > 0)
            {
                i = inventory[0];
                inventory.RemoveAt(0);    
            }
            else
            {
                i = new Item("");
            }
            return i;
        }
    }
}
