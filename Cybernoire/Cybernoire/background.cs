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
    class Background : Object
    {
        bool isSolid = false;

        public Background(String Name, bool isSolid, ObjectType Type, string textureName, Vector2 Location)
            : base(Name, isSolid, Type, textureName, Location)
        {
        }
    }
}

