using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace LineTest
{
    public class Player
    {
        #region Variables
        public Texture2D texture;
        public Vector2 position;
        public Rectangle bounds;
        Random r = new Random();
        GraphicsDeviceManager graphics;
        #endregion

        #region Constructor
        public Player(Texture2D texture, Vector2 position, GraphicsDeviceManager graphics)
        {
            this.texture = texture;
            this.position = position;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.graphics = graphics;
        }
        #endregion

        public void Initialize(){}

        public void LoadContent(ContentManager Content)
        {

        }

        bool keyDown = false;
        bool isFlapping = false;

        float velocity = 0.3f;
        public float speed = 0f;

        int newSpot;
        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            GamePadState pState = GamePad.GetState(PlayerIndex.One);
            MouseState mState = Mouse.GetState();

            #region Control
            if ((kState.IsKeyDown(Keys.Space) || mState.LeftButton == ButtonState.Pressed) && keyDown == false)
            {
                flap();
                keyDown = true;
            }
            if ((kState.IsKeyUp(Keys.Space) && mState.LeftButton == ButtonState.Released))
            {
                keyDown = false;
            }
            #endregion

            if (isFlapping)
            {
                if (bounds.Y > newSpot)
                {
                    speed = 5;
                    bounds.Y -= (int)speed;
                    speed -= 3;
                }
                else
                {
                    isFlapping = false;
                    speed = 0;
                }
            }
            else
            {
                if (speed < 5)
                {
                    speed += velocity;
                }
                bounds.Y += (int)speed;
            }
            MoveToScreen();
        }

        public void flap() 
        {
            isFlapping = true;
            newSpot = bounds.Y - 50;
            if (newSpot < 5)
            {
                newSpot = 6;
            }
        }

        public void MoveToScreen() 
        {
            /*if (bounds.X < 0)
            {
                if (bounds.X < -30)
                {
                    bounds.X += 3;
                }
                else
                {
                    bounds.X += 1;
                }
            }
            if (bounds.X + texture.Width > graphics.PreferredBackBufferWidth)
            {
                if (bounds.X + texture.Width > graphics.PreferredBackBufferWidth + 30)
                {
                    bounds.X -= 3;
                }
                else
                {
                    bounds.X -= 1;
                }
            }*/
            if (bounds.Y < 1)
            {
                bounds.Y = 2;
            }
            if (bounds.Y + texture.Height + 112 > graphics.PreferredBackBufferHeight)
            {
                bounds.Y = graphics.PreferredBackBufferHeight - texture.Height - 112;
            }
        }

        public void moveToMouse(MouseState m)
        {
            int half = bounds.Height / 2;
            if (m.Y-half > bounds.Y)
            {
                bounds.Y += 3;
            }
            if (m.Y-half < bounds.Y)
            {
                bounds.Y -= 3;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color c)
        {
            spriteBatch.Draw(texture, bounds, c);
        }
    }
}
