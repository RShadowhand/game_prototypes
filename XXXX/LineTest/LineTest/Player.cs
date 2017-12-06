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
using ObjectHelper;

namespace LineTest
{
    public class Player
    {
        #region Variables
        public Texture2D texture;
        public Vector2 position;
        public Rectangle bounds;
        public int Health = 100;
        public int Lives = 3;
        public bool invincible = false;
        Random r = new Random();
        Texture2D bulletTexture;
        public List<Bullet> bullets = new List<Bullet>();
        Bullet b;
        public float bulletSpeed = 0.01f;
        public float bulletTimer = 0.0f;
        float switchMult = 0.0f;
        Texture2D fire1, fire2, fire3, fireCur;
        Rectangle rFireCur;
        public Point pLocation, fLocation;
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
            bulletTexture = Content.Load<Texture2D>("mermi");
            fire1 = Content.Load<Texture2D>("eksoz-a");
            fire2 = Content.Load<Texture2D>("eksoz-b");
            //fire3 = Content.Load<Texture2D>("fire3_");

            fireCur = fire1;

            fLocation = new Point(pLocation.X, ((pLocation.Y + fireCur.Height / 2) - (fireCur.Height / 2)) + 5);
            rFireCur = new Rectangle(fLocation.X, fLocation.Y, fireCur.Width, fireCur.Height);
        }

        public void Fire()
        {
            b = new Bullet(bulletTexture, new Vector2(bounds.X + ((bounds.Width) - (bulletTexture.Width / 2)), bounds.Y + ((bounds.Height / 2) - (bulletTexture.Height / 2))));
            bullets.Add(b);
        }

        public Vector2 Speed = Vector2.Zero;
        public Vector2 Direction = Vector2.Zero;
        public float accelerationDeath = 15f;
        public float accelerationRate = 300.0f;
        public float speedlimit = 6.0f;

        public float invisTimer = 0.0f;

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            GamePadState pState = GamePad.GetState(PlayerIndex.One);
            MouseState mState = Mouse.GetState();

            if (invincible)
            {
                invisTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (invisTimer > 2500)
                {
                    invisTimer = 0;
                    invincible = false;
                }
            }

            #region Bullets
            foreach (var item in this.bullets)
            {
                item.position.X += 24;
                item.bounds = new Rectangle((int)item.position.X, (int)item.position.Y, item.texture.Width, item.texture.Height);
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (b.position.X > graphics.PreferredBackBufferWidth)
                {
                    bullets.RemoveAt(i);
                }
            }
            #endregion



            #region Control
            if (Speed.Y > accelerationDeath || Speed.Y < -accelerationDeath)
            {
                Speed.Y -= accelerationDeath;
            }
            else
            {
                Speed.Y = 0;
            }
            if (Speed.X > accelerationDeath || Speed.X < -accelerationDeath)
            {
                Speed.X -= accelerationDeath*2;
            }
            else
            {
                Speed.X = 0;
            }

            if (kState.IsKeyDown(Keys.Up) || pState.IsButtonDown(Buttons.DPadUp) || pState.ThumbSticks.Left.Y > 0)
            {
                Speed.Y = accelerationRate;
                Direction.Y = -1;
            }
            if (kState.IsKeyDown(Keys.Down) || pState.IsButtonDown(Buttons.DPadDown) || pState.ThumbSticks.Left.Y < 0)
            {
                Speed.Y = accelerationRate;
                Direction.Y = 1;
            }
            if (kState.IsKeyDown(Keys.Left) || pState.IsButtonDown(Buttons.DPadLeft) || pState.ThumbSticks.Left.X < 0)
            {
                Speed.X = accelerationRate;
                Direction.X = -1;
            }
            if (kState.IsKeyDown(Keys.Right) || pState.IsButtonDown(Buttons.DPadRight) || pState.ThumbSticks.Left.X > 0)
            {
                Speed.X = accelerationRate*1.3f;
                Direction.X = 1;
            }

            position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            bounds.Y = (int)position.Y;
            bounds.X = (int)position.X;

            MoveToScreen();

            fLocation = new Point(bounds.X - 5, bounds.Y + (bounds.Height / 2) - 2);// ((bounds.Y + fireCur.Height / 2) - (fireCur.Height / 2)) + 5);
            rFireCur.Location = fLocation;

            if (kState.IsKeyDown(Keys.Space) || pState.IsButtonDown(Buttons.A))
            {
                bulletTimer += bulletSpeed;
                if (bulletTimer >= 0.00f && bulletTimer <= 0.01f)
                {
                    Fire();
                }
                if (bulletTimer >= 0.15f)
                {
                    bulletTimer = 0;
                }
            }
            else
            {
                bulletTimer = 0.0f;
            }
            #endregion

            #region flameAnimation
            switchMult += 0.1f;
            if (Convert.ToInt32(switchMult) > 1)
            {
                fireCur = fire1;
            }
            if (Convert.ToInt32(switchMult) > 2)
            {
                fireCur = fire2;
                switchMult = 0;
            }
            //if (Convert.ToInt32(switchMult) > 3)
            //{
            //    fireCur = fire3;
            //    switchMult = 0;
            //}
            #endregion

        }

        public void MoveToScreen() 
        {
            if (position.X < 0)
            {
                if (position.X < -50)
                {
                    position.X += 5;
                }
                else
                {
                    position.X += 3;
                }
            }
            if (position.X + texture.Width > graphics.PreferredBackBufferWidth)
            {
                if (position.X + texture.Width > graphics.PreferredBackBufferWidth + 50)
                {
                    position.X -= 5;
                }
                else
                {
                    position.X -= 3;
                }
            }
            if (position.Y < 0)
            {
                position.Y = 1;
            }
            if (position.Y + texture.Height > graphics.PreferredBackBufferHeight)
            {
                position.Y = graphics.PreferredBackBufferHeight - texture.Height;
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
            foreach (var item in this.bullets)
            {
                item.Draw(spriteBatch);
            }
            if (invincible)
            {
                c = c * 0.3f;
            }
            spriteBatch.Draw(texture, bounds, c);
            spriteBatch.Draw(fireCur, rFireCur, Color.White);
        }
    }
}
