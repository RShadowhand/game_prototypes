using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Timers;

namespace ShooterTest
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables
        public enum GameState
        {
            MainMenu,
            Playing,
            GameOver,
        }
        CollisionEngine Collision;
        BGHandler bgHandler = new BGHandler();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ship;
        public Player player;
        public Bullet b;
        public Spawner enemySpawner = new Spawner();
        Rectangle MouseRect;
        Form1 f;
        Random r = new Random();
        SpriteFont font;
        
        public GameState gState = new GameState();
        public int Score = 0;

        Color renk;
        Timer timer1 = new Timer(250);

        public string ScoreText;

        public Texture2D explosion, empty, eActual;
        public Rectangle rExplosion;
        
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            Collision = new CollisionEngine(this);
        }

        protected override void Initialize()
        {
            gState = GameState.MainMenu;
            Collision.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Collision.LoadContent(Content);
            #region VarInit
            font = Content.Load<SpriteFont>("Font");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ship = Content.Load<Texture2D>("ship");
            f = new Form1();
            //f.Show();
            player = new Player(ship, Vector2.Zero);
            #endregion

            enemySpawner.LoadContent(Content);
            bgHandler.LoadContent(Content);
            player.LoadContent(Content);
            explosion = Content.Load<Texture2D>("explosion");
            empty = Content.Load<Texture2D>("empty");
            eActual = explosion;
            rExplosion = new Rectangle(0, 0, explosion.Width, explosion.Height);
            
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
        }

        void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            renk = new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
        }

        protected override void UnloadContent(){}

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            MouseRect = new Rectangle(mouse.X, mouse.Y, 0, 0);

            switch (gState)
            {
                case GameState.MainMenu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    {
                        gState = GameState.Playing;
                    }
                    break;
                case GameState.Playing:
                    Collision.Update(gameTime);
                    enemySpawner.Update(gameTime);
                    player.Update(gameTime);
                    if (player.Health <= 0)
                    {
                        ScoreText = "Your score: " + Score;
                        gState = Game1.GameState.GameOver;
                    }
                    break;
                case GameState.GameOver:
                    if (kState.IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    {
                        player.pLocation = new Point(0, 0);
                        player.Health = 100;
                        Score = 0;
                        gState = GameState.Playing;
                    }
                    break;
            }
            /*if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                bgHandler.backspeed += 1.0f;
                bgHandler.frontspeed += 1.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                bgHandler.backspeed -= 1.0f;
                bgHandler.frontspeed -= 1.0f;
            }*/
            bgHandler.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            #region FormLogic
            //Form1 Logic
            f.label1.Text = "X: "+ Mouse.GetState().X.ToString();
            f.label2.Text = "Y: " + Mouse.GetState().Y.ToString();
            f.label3.Text = "Left: " + Mouse.GetState().LeftButton;
            f.label4.Text = "Middle: " + Mouse.GetState().MiddleButton;
            f.label5.Text = "Right: " + Mouse.GetState().RightButton;
            f.label6.Text = "X: " + player.pLocation.X.ToString();
            f.label8.Text = "BulletCount: " + player.bullets.Count();
            f.label7.Text = "Y: " + player.pLocation.Y.ToString();
            f.label10.Text = "bulletimer: " + player.bulletTimer.ToString();
            //Form1 Logic End
            #endregion

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            bgHandler.Draw(spriteBatch);
            Collision.Draw(spriteBatch);
            switch (gState) { 
                case GameState.MainMenu:
                    spriteBatch.DrawString(font, "Press Space to Start", new Vector2(0, Window.ClientBounds.Height - 23), renk);
                   // spriteBatch.Draw(eActual, rExplosion, Color.White);
                    break;
                case GameState.Playing:
                    enemySpawner.Draw(spriteBatch);
                    player.Draw(spriteBatch);

                    spriteBatch.DrawString(font, Score.ToString(), Vector2.Zero, Color.Red);
                    spriteBatch.DrawString(font, player.Health.ToString(), new Vector2(0, 20), Color.Red);
                    break;
                case GameState.GameOver:
                    spriteBatch.DrawString(font, "Press Space to Start", new Vector2(0, Window.ClientBounds.Height - 23), renk);
                    spriteBatch.DrawString(font, ScoreText, new Vector2(Window.ClientBounds.Width / 2 - 5*ScoreText.Length, Window.ClientBounds.Height/2-3), renk);  
                    break;
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
