using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ObjectEngine
{
    public class GameObject
    {
        public Vector2 pos;
        public string firsttexture;
        public string texturepath;
        public Texture2D rTexture, TextOne, TextTwo;
        Rectangle Rect;
        ContentManager Content;
        public bool Selected = false;

        public GameObject(Vector2 pos, string texture, string texturepath, ContentManager Content)
        {
            this.pos = pos;
            this.firsttexture = texture;
            this.texturepath = texturepath;
            this.Content = Content;
            if (texturepath == "")
            {
                TextOne = Content.Load<Texture2D>(firsttexture);
            }
            else
            {
                TextOne = Content.Load<Texture2D>(texturepath + "/" + firsttexture);
            }
            rTexture = TextOne;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, rTexture.Width, rTexture.Height);
        }

        public void Move(float x, float y)
        {
            Rect.Location = new Point(
                Rect.Location.X + (int)x, 
                Rect.Location.Y + (int)y);
        }

        public void MoveTo(float x, float y)
        {
            Rect.Location = new Point((int)x, (int)y);
        }

        public Vector2 getPosition() {
            return new Vector2(Rect.Location.X, Rect.Location.Y);
        }

        public Rectangle getRect() {
            return Rect;
        }

        public void setRect(Rectangle rect) {
            Rect = rect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rTexture, Rect, Color.White);
        }

        public void AssignSecondTexture(string secondtexture) {
            TextTwo = Content.Load<Texture2D>(texturepath + "/" + secondtexture);
        }

        public void SwitchTexture() {
            if (rTexture == TextOne)
            {
                rTexture = TextTwo;
            }
            else
            {
                rTexture = TextOne;
            }
        }

        public void SwitchToFirst() 
        {
            rTexture = TextOne;
        }
        public void SwitchToSecond()
        {
            rTexture = TextTwo;
        }

        public void Select() { Selected = true; }

        public void Deselect() { Selected = false; }

    }
}
