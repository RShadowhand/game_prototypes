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
    class BGHandler
    {

        Background Background00;
        Background Background01;
        Background Background10;
        Background Background11;

        public float backspeed = 1.5f, frontspeed = 3f;

        public void LoadContent(ContentManager Content)
        {
            Background00 = new Background(Content.Load<Texture2D>("bg"), Vector2.Zero);
            Background01 = new Background(Content.Load<Texture2D>("bg"), Vector2.Zero);
            Background10 = new Background(Content.Load<Texture2D>("ground"), new Vector2(0, 512 - 112));
            Background11 = new Background(Content.Load<Texture2D>("ground"), new Vector2(0, 512 - 112));
        }

        public void Update(GameTime gameTime)
        {
            #region Background Stuff
            Background00.position.X -= backspeed;
            Background01.position.X -= backspeed;
            Background10.position.X -= frontspeed;
            Background11.position.X -= frontspeed;

            if (Background00.position.X + Background00.bounds.Width < 288)
            {
                Background01.position.X = Background00.position.X + Background00.bounds.Width;
            }
            if (Background01.position.X + Background01.bounds.Width < 288)
            {
                Background00.position.X = Background01.position.X + Background01.bounds.Width;
            }

            if (Background10.position.X + Background10.bounds.Width < 288)
            {
                Background11.position.X = Background10.position.X + Background10.bounds.Width;
            }
            if (Background11.position.X + Background11.bounds.Width < 288)
            {
                Background10.position.X = Background11.position.X + Background11.bounds.Width;
            }
            Background00.bounds = new Rectangle((int)Background00.position.X, (int)Background00.position.Y, Background00.texture.Width, Background00.texture.Height);
            Background01.bounds = new Rectangle((int)Background01.position.X, (int)Background01.position.Y, Background01.texture.Width, Background01.texture.Height);
            Background10.bounds = new Rectangle((int)Background10.position.X, (int)Background10.position.Y, Background10.texture.Width, Background10.texture.Height);
            Background11.bounds = new Rectangle((int)Background11.position.X, (int)Background11.position.Y, Background11.texture.Width, Background11.texture.Height);
            #endregion
        }

        public void Draw1(SpriteBatch spriteBatch, Color c)
        {
            Background00.Draw(spriteBatch,c);
            Background01.Draw(spriteBatch,c);
        }
        public void Draw2(SpriteBatch spriteBatch, Color c) 
        {
            Background10.Draw(spriteBatch, c);
            Background11.Draw(spriteBatch, c);
        }
    }
}

