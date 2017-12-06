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
        Texture2D buffer;
        bool Switch = false;

        Color[] arr, arr2;
        Random r = new Random();
        int x = 200, y = 100;
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
            t2 = new Texture2D(GraphicsDevice, texture.Width, texture.Height);
            arr = new Color[texture.Width * texture.Height];
            arr2 = new Color[texture.Width * texture.Height];

            texture.GetData(arr);

            t2.SetData(arr);
            t2.GetData(arr2);

            
            int constant = 30;
            for (int i = texture.Height / 2; i < texture.Height; i++)
            {
                for (int j = 0; j < texture.Width - (constant + 1); j++)
                {
                    if (arr[i * texture.Width + j] != Color.Transparent)
                    {

                        arr[(i * texture.Width + j)] = arr[i * texture.Width + j + constant];
                        
                    }
                }
                for (int j = texture.Width - (constant+1); j < texture.Width; j++)
                {
                    arr[(i * texture.Width + j)] = Color.Transparent;
                }
            }
            texture.SetData(arr);
            
        }

        protected override void UnloadContent(){}

        KeyboardState ks;
        bool keyDown = false;
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (keyDown == false)
            {
                if (ks.IsKeyDown(Keys.Space))
                {
                    if (Switch)
                    {
                        Switch = false;
                    }
                    else
                    {
                        Switch = true;
                    }

                    buffer = texture;
                    texture = t2;
                    t2 = buffer;
                }
            }
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr2[i] != Color.Transparent)
                {
                    arr2[i] = Color.FromNonPremultiplied(r.Next(), r.Next(), r.Next(), r.Next());
                }
            }

            if (!Switch)
            {
                t2.SetData(arr2);
            }
            else
            {
                texture.SetData(arr2);
            }

            if (ks.GetPressedKeys().Length > 0)
            {
                keyDown = true;
            }
            else
            {
                keyDown = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(100, 100, texture.Width, texture.Height), Color.White);
            spriteBatch.Draw(t2, new Rectangle(x, y, t2.Width, t2.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
