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


namespace ShooterTest
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player
    {
        #region Variables
        public Texture2D texture;
        public Vector2 position;
        public Rectangle bounds;
        public int Health = 100;
        Random r = new Random();
        Texture2D bulletTexture;
        public List<Bullet> bullets = new List<Bullet>();
        Bullet b;
        Game1 main = new Game1();
        public float bulletSpeed = 0.01f;
        public float bulletTimer = 0.0f;
        float switchMult = 0.0f;
        Texture2D fire1, fire2, fire3, fireCur;
        Rectangle rFireCur;
        public Point pLocation, fLocation;
        #endregion

        #region Constructor
        public Player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        #endregion

        public void Initialize(){}

        public void LoadContent(ContentManager Content)
        {
            bulletTexture = Content.Load<Texture2D>("bullet");
            fire1 = Content.Load<Texture2D>("fire1_");
            fire2 = Content.Load<Texture2D>("fire2_");
            fire3 = Content.Load<Texture2D>("fire3_");

            fireCur = fire1;

            fLocation = new Point(pLocation.X, ((pLocation.Y + fireCur.Height / 2) - (fireCur.Height / 2)) + 5);
            rFireCur = new Rectangle(fLocation.X, fLocation.Y, fireCur.Width, fireCur.Height);
        }

        public void Fire()
        {
            b = new Bullet(bulletTexture, new Vector2(this.pLocation.X + ((this.bounds.Width) - (bulletTexture.Width / 2)), this.pLocation.Y + ((this.bounds.Height / 2) - (bulletTexture.Height / 2))));
            bullets.Add(b);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            GamePadState pState = GamePad.GetState(PlayerIndex.One);

            #region Bullets
            foreach (var item in this.bullets)
            {
                item.position.X += 5;
                item.bounds = new Rectangle((int)item.position.X, (int)item.position.Y, item.texture.Width, item.texture.Height);
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (b.position.X > main.Window.ClientBounds.Width)
                {
                    bullets.RemoveAt(i);
                }
            }
            #endregion

            #region Control
                if (kState.IsKeyDown(Keys.Up) || pState.IsButtonDown(Buttons.DPadUp) || pState.ThumbSticks.Left.Y > 0)
                {
                    pLocation.Y -= 3;
                }
                if (kState.IsKeyDown(Keys.Down) || pState.IsButtonDown(Buttons.DPadDown) || pState.ThumbSticks.Left.Y < 0)
                {
                    pLocation.Y += 3;
                }
                if (kState.IsKeyDown(Keys.Left) || pState.IsButtonDown(Buttons.DPadLeft) || pState.ThumbSticks.Left.X < 0)
                {
                    pLocation.X -= 3;
                }
                if (kState.IsKeyDown(Keys.Right) || pState.IsButtonDown(Buttons.DPadRight) || pState.ThumbSticks.Left.X > 0)
                {
                    pLocation.X += 3;
                }
            
            bounds.Location = pLocation;
            fLocation = new Point(pLocation.X, ((pLocation.Y + fireCur.Height / 2) - (fireCur.Height / 2)) + 5);
            rFireCur.Location = fLocation;

            if (kState.IsKeyDown(Keys.Space) || pState.IsButtonDown(Buttons.A))
            {
                bulletTimer += bulletSpeed;
                if (bulletTimer >= 0.00f && bulletTimer <= 0.01f)
                {
                    Fire();
                }
                if (bulletTimer >= 0.2f)
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
            }
            if (Convert.ToInt32(switchMult) > 3)
            {
                fireCur = fire3;
                switchMult = 0;
            }
            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item  in this.bullets)
            {
                item.Draw(spriteBatch);
            }

            spriteBatch.Draw(texture, this.bounds, Color.White);
            spriteBatch.Draw(fireCur, rFireCur, Color.White);
        }
    }
}
