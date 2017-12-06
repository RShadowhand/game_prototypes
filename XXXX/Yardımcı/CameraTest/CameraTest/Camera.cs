using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace CameraTest
{
    class Camera
    {
        Vector2 Position;
        public Matrix transform;
        GraphicsDeviceManager graphics;
        string Coords, Size;
        float zoomAmount;
        float Width, Height;

        public Camera(Vector2 Position, GraphicsDeviceManager graphics)
        {
            this.Position = Position;
            this.graphics = graphics;
            Width = graphics.PreferredBackBufferWidth;
            Height = graphics.PreferredBackBufferHeight;
            Size = Width.ToString() + " - " + Height.ToString();
            zoomAmount = 1.0f;
            
        }

        public void Update(GameTime gt, MouseState mState)
        {
            Coords = Position.X + " - " + Position.Y;
            if (zoomAmount < 0.1f)
            {
                zoomAmount = 0.1f;
            }
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void ZoomIn() { zoomAmount += 0.1f; }
        public void ZoomOut() { zoomAmount -= 0.1f; }

        public void SetZoom(float Amount) { zoomAmount = Amount; }

        public Matrix get_transformation()
        {
            transform =
              Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
              Matrix.CreateScale(new Vector3(zoomAmount, zoomAmount, 1)) *
              Matrix.CreateTranslation(new Vector3(Width * 0.5f, Height * 0.5f, 0));
            return transform;
        }

        public void Begin(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        get_transformation());
        }

        public void Draw(SpriteBatch sb, SpriteFont font) {
            sb.DrawString(font, Coords, Vector2.Zero, Color.Black);
            sb.DrawString(font, Size, new Vector2(0,40), Color.Black);
        }
    }
}
