using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace arkanoid
{
    class floatRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public floatRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Top
        {
            get { return Y; }
        }
        public float Bottom
        {
            get { return Y + Height; }
        }
        public float Left
        {
            get { return X; }
        }
        public float Right
        {
            get { return X + Width; }
        }

        public bool Intersects(floatRectangle Rectangle) 
        {
            return (Rectangle.X < this.X + this.Width) &&
            (this.X < (Rectangle.X + Rectangle.Width)) &&
            (Rectangle.Y < this.Y + this.Height) &&
            (this.Y < Rectangle.Y + Rectangle.Height); 
        }

        public bool Intersects(Rectangle Rectangle)
        {
            return (Rectangle.X < this.X + this.Width) &&
            (this.X < (Rectangle.X + Rectangle.Width)) &&
            (Rectangle.Y < this.Y + this.Height) &&
            (this.Y < Rectangle.Y + Rectangle.Height); 
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
        }
    }
}
