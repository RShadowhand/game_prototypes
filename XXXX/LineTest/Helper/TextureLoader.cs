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

namespace ObjectHelper
{
    public static class TextureLoader
    {
        public static List<Texture2D> LoadTextures(ContentManager Content, string file, string path)
        {
            List<Texture2D> a = new List<Texture2D>();
            string list = new StreamReader(file).ReadToEnd();
            string[] parsed = Parse(list, ";");
            for (int i = 0; i < parsed.Length; i++)
            {
                if (parsed[i].Contains("\r\n"))
                {
                    parsed[i] = (parsed[i].Substring(2, parsed[i].Length - 2));
                }
                Texture2D t = Content.Load<Texture2D>(path + "/" + parsed[i]);
                t.Name = parsed[i];
                a.Add(t);
            }
            return a;
        }

        public static List<string> TextureList(string file)
        {
            string list = new StreamReader(file).ReadToEnd();
            string[] parsed = Parse(list, ";");
            for (int i = 0; i < parsed.Length; i++)
            {
                if (parsed[i].Contains("\r\n"))
                {
                    parsed[i] = (parsed[i].Substring(2, parsed[i].Length - 2));
                }
            }
            return parsed.ToList<string>();
        }

        public static string[] Parse(string toBeParsed, string Condition) { return toBeParsed.Split(new string[] { Condition }, StringSplitOptions.RemoveEmptyEntries); }
    }
}
