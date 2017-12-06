#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace LineTest
{
    public enum GameState
    {
        menu, playing, dead
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BGHandler bgHandler = new BGHandler();
        public Player player;
        public Spawner enemySpawner;
        public int Score = 0;
        CollisionEngine ce;
        public GameState gs = GameState.menu;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ce = new CollisionEngine(this);
        }
        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 288;
            graphics.PreferredBackBufferHeight = 512;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("player"), new Vector2(50, 0), graphics);
            enemySpawner = new Spawner(graphics, Content);
            bgHandler.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        KeyboardState ks;
        bool keyDown = false;

        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            switch (gs)
            {
                case GameState.menu:
                    bgHandler.Update(gameTime);
                    if ((ks.IsKeyDown(Keys.Space) || mState.LeftButton == ButtonState.Pressed) && keyDown == false)
                    {
                        keyDown = true;
                        gs = GameState.playing;
                        enemySpawner = new Spawner(graphics, Content);
                    }
                    if ((ks.IsKeyUp(Keys.Space) && mState.LeftButton == ButtonState.Released))
                    {
                        keyDown = false;
                    }
                    
                    break;
                case GameState.playing:
                    enemySpawner.Update(gameTime);
                    player.Update(gameTime);
                    bgHandler.Update(gameTime);
                    ce.Update(gameTime);
                    break;
                case GameState.dead:
                    bgHandler.Update(gameTime);
                    if ((ks.IsKeyDown(Keys.Space) || mState.LeftButton == ButtonState.Pressed) && keyDown == false)
                    {
                        keyDown = true;
                        gs = GameState.playing;
                    }
                    if ((ks.IsKeyUp(Keys.Space) && mState.LeftButton == ButtonState.Released))
                    {
                        keyDown = false;
                        enemySpawner = new Spawner(graphics, Content);
                    }
                    break;
                default:
                    break;
            }
            
            base.Update(gameTime);
        }

        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (gs)
            {
                case GameState.menu:
                    bgHandler.Draw1(spriteBatch, Color.White);
                    bgHandler.Draw2(spriteBatch, Color.White);
                    spriteBatch.DrawString(Content.Load<SpriteFont>("Font"), "Press Space!", Vector2.Zero, Color.Black);
                    break;
                case GameState.playing:
                    bgHandler.Draw1(spriteBatch, Color.White);
                    enemySpawner.Draw(spriteBatch);
                    bgHandler.Draw2(spriteBatch, Color.White);
                    player.Draw(spriteBatch, Color.White);
                    spriteBatch.DrawString(Content.Load<SpriteFont>("Font"), enemySpawner.Score.ToString(), Vector2.Zero, Color.Black);
                    break;
                case GameState.dead:
                    bgHandler.Draw1(spriteBatch, Color.White);
                    bgHandler.Draw2(spriteBatch, Color.White);
                    spriteBatch.DrawString(Content.Load<SpriteFont>("Font"), "Your score was "+Score.ToString()+"! Again?", Vector2.Zero, Color.Black);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
