#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ObjectHelper;
#endregion

namespace LineTest
{
    public class Game1 : Game
    {
        int totalFrames = 0;
        float timePassed = 0;

        enum GameState { mainmenu, playing, dead }

        GameState gs = GameState.playing;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Color c, playerC, bgC;
        Random rand = new Random();
        Texture2D tPlayer;
        bool isKeyDown = false;
        bool playerCU = true, bgCU = true;
        bool debugMode = false;

        CollisionEngine ce;

        public int Score = 0;

        public Spawner enemySpawner;

        int r = 0, g = 0, b = 0, a = 255;

        BGHandler bgHandler = new BGHandler();
        public Player player;

        Color[] glitch = new Color[800 * 600];
        Color AlphaWhite = new Color(Color.White, 1.0f);

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        GameObjectCollection gobs;

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.ToggleFullScreen();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ce = new CollisionEngine(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            tPlayer = Content.Load<Texture2D>("dama");
            enemySpawner = new Spawner(graphics);
            player = new Player(tPlayer, new Vector2(-350, 274), graphics);
            player.invincible = true;
            bgHandler.LoadContent(Content);
            r = Color.LightGreen.R;
            g = Color.LightGreen.G;
            b = Color.LightGreen.B;
            a = Color.LightGreen.A;
            bgC = c = Color.FromNonPremultiplied(r, g, b, a);
            playerC = Color.White;
            if (playerCU == false)
            {
                playerC = Color.White;
            }
            
            player.LoadContent(Content);
            ce.LoadContent(Content);
            enemySpawner.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        KeyboardState ks;

        public void changeColor() 
        {
            c = Color.FromNonPremultiplied(r, g, b, a);
            if (playerCU)
            {
                playerC = c;
            }
            if (bgCU)
            {
                bgC = c;
            }
        }

        public void randColor()
        {
            r = rand.Next(0, 255);
            g = rand.Next(0, 255);
            b = rand.Next(0, 255);
            changeColor();
        }

        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region ColorKeys
            if (ks.IsKeyDown(Keys.Q))
            {
                if (r<255)
                {
                    r++;
                    changeColor();
                }
            }
            else if(ks.IsKeyDown(Keys.A))
            {
                if (r>0)
                {
                    r--;
                    changeColor();
                }
            }
            if (ks.IsKeyDown(Keys.W))
            {
                if (g<255)
                {
                    g++; 
                    changeColor();
                }
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                if (g>0)
                {
                    g--;
                    changeColor();
                }
            }
            if (ks.IsKeyDown(Keys.E))
            {
                if (b<255)
                {
                    b++;
                    changeColor();
                }
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                if (b>0)
                {
                    b--;
                    changeColor();
                }
            }
            if (ks.IsKeyDown(Keys.R))
            {
                if (a<255)
                {
                    a++;
                    changeColor();
                }
            }
            else if (ks.IsKeyDown(Keys.F))
            {
                if (a>0)
                {
                    a--;
                    changeColor();
                }
            }
            if (ks.IsKeyDown(Keys.C) && isKeyDown == false)
            {
                randColor();
                isKeyDown = true;
            }
            if (ks.IsKeyDown(Keys.X))
            {
                randColor();
            }
            if (ks.IsKeyDown(Keys.B) && isKeyDown == false)
            {
                if (playerCU)
                {
                    playerCU = false;
                    playerC = Color.White;
                }
                else
                {
                    playerCU = true;
                }
                changeColor();
                isKeyDown = true;
            }


            if (ks.IsKeyDown(Keys.N) && isKeyDown == false)
            {
                if (bgCU)
                {
                    bgCU = false;
                    bgC = Color.White;
                }
                else
                {
                    bgCU = true;
                }
                changeColor();
                isKeyDown = true;
            }

            if (ks.IsKeyDown(Keys.P) && isKeyDown == false)
            {
                if (debugMode)
                {
                    debugMode = false;
                }
                else
                {
                    debugMode = true;
                }
                isKeyDown = true;
            }

            if (ks.IsKeyUp(Keys.B) && ks.IsKeyUp(Keys.N) && ks.IsKeyUp(Keys.C) && ks.IsKeyUp(Keys.P))
            {
                isKeyDown = false;
            }

            #endregion

            switch (gs)
            {
                case GameState.mainmenu:
                    break;
                case GameState.playing:
                    player.Update(gameTime);
                    if (player.Health <= 0)
                    {
                        if (player.Lives <= 0)
                        {
                            gs = GameState.dead;
                        }
                        player.Lives -= 1;
                        player.Health = 100;
                        player.position.Y = 274;
                        player.position.X = -350;
                        player.invincible = true;
                    }
                    enemySpawner.Update(gameTime);
                    ce.Update(gameTime);
                    break;
                case GameState.dead:
                    if (ks.IsKeyDown(Keys.Enter))
                    {
                        Score = 0;
                        player.Lives = 3;
                        player.Health = 100;
                        player.position.Y = 274;
                        player.position.X = -350;
                        player.invincible = true;
                        gs = GameState.playing;
                    }
                    break;
                default:
                    break;
            }

            bgHandler.Update(gameTime);
            base.Update(gameTime);
            totalFrames += 1;
            timePassed += gameTime.ElapsedGameTime.Milliseconds;
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            bgHandler.Draw(spriteBatch, bgC);

            if(debugMode == true){
                spriteBatch.DrawString(font, "Red:" + r.ToString(), Vector2.Zero, Color.White);
                spriteBatch.DrawString(font, "Green:" + g.ToString(), new Vector2(0, 20), Color.White);
                spriteBatch.DrawString(font, "Blue:" + b.ToString(), new Vector2(0, 40), Color.White);
                spriteBatch.DrawString(font, "Alpha:" + a.ToString(), new Vector2(0, 60), Color.White);
                spriteBatch.DrawString(font, "frames:" + ((int)(totalFrames/(timePassed/1000))).ToString(), new Vector2(0, 80), Color.White);
            }
            switch (gs)
            {
                case GameState.mainmenu:
                    break;
                case GameState.playing:
                    player.Draw(spriteBatch, playerC);
                    enemySpawner.Draw(spriteBatch);
                    ce.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Score: " + Score.ToString(), Vector2.Zero, Color.White);
                    spriteBatch.DrawString(font, "Lives: " + player.Lives.ToString(), new Vector2(0, 27), Color.White);
                    break;
                case GameState.dead:
                    spriteBatch.DrawString(font, "Your score was " + Score.ToString(), new Vector2(1280/2-100, 720/2), Color.White);
                    spriteBatch.DrawString(font, "Press Enter to begin again.", new Vector2(10, 720 - 30), Color.White);
                    break;
                default:
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
