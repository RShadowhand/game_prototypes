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
using System.IO;
using ObjectEngine;

namespace ObjectEngine
{
    public static class ObjectFactory
    {
        public static GameObjectCollection MakeCollection(string file, string contentpath, ContentManager Content)
        {
            string list = new StreamReader(file).ReadToEnd();
            GameObjectCollection b = new GameObjectCollection();
            List<string> temp = new List<string>();
            List<string> parsed1 = new List<string>(Parse(list, ";"));
            List<string> parsed2 = new List<string>();
            
            Dictionary<string, Vector2> objectCreator = new Dictionary<string, Vector2>();
            
            foreach (string i in parsed1)
            {
                if (i.Contains("\r\n"))
                {
                    parsed2.Add(i.Substring(2, i.Length-2));
                }
                else
                {
                    parsed2.Add(i);
                }
            }
            
            parsed1 = new List<string>(parsed2);
            parsed2.Clear();

            foreach (string i in parsed1)
            {
                string[] p = Parse(i, ":");
                parsed2.Add(p[1]);
                temp.Add(p[0]);
            }

            parsed1 = new List<string>(temp);

            for (int i = 0; i < parsed1.Count; i++)
            {
                string[] p = Parse(parsed2[i], ",");
                objectCreator.Add(parsed1[i], new Vector2(float.Parse(p[0]), float.Parse(p[1])));
            }

            foreach (string key in objectCreator.Keys)
            {
                b.Items.Add(key, new GameObject(objectCreator[key], key, contentpath, Content));
            }

            temp.Clear();
            parsed1.Clear();
            parsed2.Clear();
            objectCreator.Clear();

            return b;
        }

        public static string[] Parse(string toBeParsed, string Condition) { return toBeParsed.Split(new string[] { Condition }, StringSplitOptions.RemoveEmptyEntries); }
    }
}
