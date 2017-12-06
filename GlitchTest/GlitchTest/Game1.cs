#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace GlitchTest
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texture;
        Texture2D t2;
        Color[] arr, arr2;
        Random r = new Random();
        int a = 0;
        SpriteFont myFont;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("dama");
            //t2 = Content.Load<Texture2D>("dama");
            t2 = new Texture2D(GraphicsDevice, texture.Width, texture.Height);
            arr = new Color[texture.Width * texture.Height];
            arr2 = new Color[(texture.Width*2) * (texture.Height*2)];
            texture.GetData(arr);
            texture.GetData(arr2);
            t2.SetData(arr2);
            myFont = Content.Load<SpriteFont>("Font");
        }

        protected override void UnloadContent(){}

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    if (arr[i] != Color.Transparent)
            //    {
            //        arr[i] = Color.FromNonPremultiplied(r.Next(), r.Next(), r.Next(), r.Next());
            //    }
            //}
            MouseState ms = Mouse.GetState();
            if (ms.X < texture.Width && ms.Y < texture.Height)
            {
                int coord = (texture.Width * ms.Y) + ms.X;
                arr[coord] = Color.FromNonPremultiplied(r.Next(), r.Next(), r.Next(), 255);
            }
            
            
            texture.SetData(arr);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(texture, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            spriteBatch.Draw(t2, new Rectangle(200, 100, t2.Width, t2.Height), Color.White);
            //spriteBatch.DrawString(myFont, t2.Width.ToString() + " - " +t2.Height.ToString(), Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
