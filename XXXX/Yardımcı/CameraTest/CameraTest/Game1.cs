#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ObjectEngine;
#endregion

namespace CameraTest
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject block;
        Camera cam;
        public SpriteFont font;
        Vector2 CamMover = new Vector2(0,0);

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;
            graphics.IsFullScreen = false;
            if (graphics.IsFullScreen == true)
            {
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
            }
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            block = new GameObject(Vector2.Zero, "block", "", Content);
            cam = new Camera(Vector2.Zero, graphics);
            font = Content.Load<SpriteFont>("Font1");
        }

        protected override void UnloadContent(){}

        KeyboardState kState;
        protected override void Update(GameTime gameTime)
        {
            kState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


                if (kState.IsKeyDown(Keys.Left))
                {
                    cam.Move(new Vector2(-2, 0));
                }
                if (kState.IsKeyDown(Keys.Right))
                {
                    cam.Move(new Vector2(2, 0));
                }
                if (kState.IsKeyDown(Keys.Up))
                {
                    cam.Move(new Vector2(0, -2));
                }
                if (kState.IsKeyDown(Keys.Down))
                {
                    cam.Move(new Vector2(0, 2));
                }

                if (kState.IsKeyDown(Keys.D))
                {
                    block.Move(1, 0);
                }
                if (kState.IsKeyDown(Keys.A))
                {
                    block.Move(-1, 0);
                }
                if (kState.IsKeyDown(Keys.W))
                {
                    block.Move(0, -1);
                }
                if (kState.IsKeyDown(Keys.S))
                {
                    block.Move(0, 1);
                }

                if (kState.IsKeyDown(Keys.Enter))
                {
                    graphics.ToggleFullScreen();
                    AdjustResolution();
                    graphics.ApplyChanges();
                }

                if (kState.IsKeyDown(Keys.NumPad8))
                {
                    cam.ZoomIn();
                }
                if (kState.IsKeyDown(Keys.NumPad2))
                {
                    cam.ZoomOut();
                }

            cam.Update(gameTime, Mouse.GetState());
            base.Update(gameTime);
        }

        void AdjustResolution() 
        {
            if (graphics.IsFullScreen == true)
            {
                graphics.PreferredBackBufferWidth = 1366;
                graphics.PreferredBackBufferHeight = 768;
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            cam.Begin(spriteBatch);
            block.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            cam.Draw(spriteBatch, font);
            spriteBatch.DrawString(font, "", new Vector2(0, 50), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
