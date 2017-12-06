#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace arkanoid
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        Rectangle screenspace = new Rectangle(new Point(0, 0), new Point(800, 600));

        Texture2D[] tiles = new Texture2D[5];
        Rectangle[,] rTiles = new Rectangle[5, 16];
        Color[,] tileColors = new Color[5, 16];


        Texture2D pixel;
        Rectangle rPixel;

        Texture2D player;
        Rectangle rPlayer;

        Texture2D top;
        Rectangle rTop;
        Vector2 vTop = new Vector2(300, 300);

        SpriteFont f;
        Random r = new Random();

        BeatController bc = new BeatController(120, new TimeSpan(0, 0, 30));
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            bc.LoadContent(Content);
            
            //graphics.ToggleFullScreen();

            f = Content.Load<SpriteFont>("Font");

            pixel = Content.Load<Texture2D>("pixel");
            rPixel = new Rectangle(graphics.PreferredBackBufferWidth - 100, 520, 30, 80);

            for (int i = 0; i < 5; i++)
            {
                tiles[i] = Content.Load<Texture2D>("bl1_lvl1");// + (i + 1).ToString());
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    rTiles[i, j] = new Rectangle(j * tiles[i].Width, i * tiles[i].Height, tiles[i].Width, tiles[i].Height);
                    tileColors[i, j] = new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                }
            }

            player = Content.Load<Texture2D>("pd1");
            rPlayer = new Rectangle(50, 400, player.Width, player.Height);

            top = Content.Load<Texture2D>("top");
            rTop = new Rectangle(300, 300, (int)(top.Width*1.5f), (int)(top.Height*1.5));
        }

        protected override void UnloadContent(){}

        
        float xdirection = 1.0f;
        float ydirection = 1.0f;
        int power = 5;

        bool gameStarted = false;

        MouseState ms;
        KeyboardState ks;

        int clicks = 10;

        TimeSpan oMTime = new TimeSpan();
        TimeSpan mTime = new TimeSpan();
        int beatms = 500;

        Color background = Color.Gray;


        #region buttonlocks
        MouseState oldMS;
        #endregion
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();

            bc.Update(gameTime);


            mTime += gameTime.ElapsedGameTime;
            if (gameStarted)
            {
                /*if (clicks == 0)
                {
                    if (mTime.Milliseconds > beatms)
                    {
                        clicks = 0;
                        mTime = new TimeSpan(0, 0, 0);
                        if (background == Color.Gray)
                        {
                            background = Color.Black;
                        }
                        else
                        {
                            background = Color.Gray;
                        }
                    }
                }
                else
                {
                    if (mTime.Milliseconds > beatms + 150)
                    {
                        clicks = 0;
                        mTime = new TimeSpan(0, 0, 0);
                        if (background == Color.Gray)
                        {
                            background = Color.Black;
                        }
                        else
                        {
                            background = Color.Gray;
                        }
                    }
                }*/
                foreach (Beat b in bc.beats)
                {
                    if (b.r.Intersects(rPixel))
                    {
                        if (ms.LeftButton == ButtonState.Pressed && oldMS.LeftButton == ButtonState.Released)
                        {
                            clicks++;
                            if (background == Color.Gray)
                            {
                                background = Color.Black;
                            }
                            else
                            {
                                background = Color.Gray;
                            }
                            break;
                        }
                        else
                        {
                            clicks = 0;
                        }
                    }
                    oMTime = mTime;
                    /*if (mTime.Milliseconds > beatms - 110 && mTime.Milliseconds < beatms + 110)
                    {
                        clicks++;

                        if (background == Color.Gray)
                        {
                            background = Color.Black;
                        }
                        else
                        {
                            background = Color.Gray;
                        }
                    }
                    else
                    {
                        clicks = 0;
                    }*/
                    mTime = new TimeSpan(0, 0, 0);
                }
            }
            
            oldMS = ms;

            if (ms.LeftButton == ButtonState.Pressed)
            {
                gameStarted = true;
            }
            if (ks.IsKeyDown(Keys.Escape)) { Exit(); }
            if (ks.IsKeyDown(Keys.P))
            {
                gameStarted = false;
            }
            rPlayer.X = ms.X - rPlayer.Width / 2;

            #region ball
            if (rTop.Bottom >= screenspace.Bottom)
            {
                vTop.Y = screenspace.Bottom - rTop.Height;
                ydirection = -1;
            }
            if (rTop.Top <= 0)
            {
                vTop.Y = 0;
                ydirection = 1;
            }
            if (rTop.Right >= screenspace.Right)
            {
                vTop.X = screenspace.Right - rTop.Width;
                xdirection *= -1;
            }
            if (rTop.Left <= 0)
            {
                vTop.X = 0;
                xdirection *= -1;
            }
            if (rTop.Intersects(rPlayer))
            {
                xdirection = (rTop.Center.X - rPlayer.Center.X) / 50.0f;
                ydirection = -1;
            }
            if (gameStarted)
            {
                vTop.X += xdirection * power;
                vTop.Y += ydirection * power;
                rTop.X = (int)vTop.X;
                rTop.Y = (int)vTop.Y;
            }
            #endregion

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (rTop.Intersects(rTiles[i, j]))
                    {
                        Rectangle r = rTiles[i, j];
                        rTiles[i, j].X = -100;
                        rTiles[i, j].Y = -100;
                        if (rTop.Top <= r.Bottom && rTop.Bottom > r.Bottom)
                        {
                            ydirection *= -1;
                            break;
                        }
                        else if (rTop.Bottom >= r.Top && rTop.Top < r.Top)
                        {
                            ydirection *= -1;
                            break;
                        }
                        if (rTop.Left <= r.Right && rTop.Right > r.Right)
                        {
                            xdirection *= -1;
                            break;
                        }
                        else if (rTop.Right >= r.Left && rTop.Left < r.Left)
                        {
                            xdirection *= -1;
                            break;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);
            spriteBatch.Begin();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    spriteBatch.Draw(tiles[i], rTiles[i, j], tileColors[i,j]);
                }
            }

            spriteBatch.Draw(player, rPlayer, Color.White);
            spriteBatch.Draw(top, rTop, Color.White);

            bc.Draw(spriteBatch);

            spriteBatch.Draw(pixel, rPixel, Color.Red);

            spriteBatch.DrawString(f, (clicks).ToString() + " - " + mTime.Milliseconds.ToString() + " - " + beatms.ToString() + " - " + oMTime.Milliseconds.ToString(), Vector2.Zero, Color.Red);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
