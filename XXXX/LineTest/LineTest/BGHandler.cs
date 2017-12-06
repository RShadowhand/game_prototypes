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

namespace LineTest
{
    class BGHandler
    {

        Background Background00;
        Background Background01;
        Background Background10;
        Background Background11;//, Background12, Background13, Background14, Background15;

        public float backspeed = 3.0f, frontspeed = 7.0f;

        public void LoadContent(ContentManager Content)
        {
            Background00 = new Background(Content.Load<Texture2D>("BG_arka"), Vector2.Zero);
            Background01 = new Background(Content.Load<Texture2D>("BG_arka"), new Vector2(3000, 0));
            Background10 = new Background(Content.Load<Texture2D>("BG_1"), Vector2.Zero);
            Background11 = new Background(Content.Load<Texture2D>("BG_4"), new Vector2(3000, 0));
            /*Background12 = new Background(Content.Load<Texture2D>("BG_2"), new Vector2(3000, 0));
            Background13 = new Background(Content.Load<Texture2D>("BG_3"), new Vector2(3000, 0));
            Background14 = new Background(Content.Load<Texture2D>("BG_4"), new Vector2(3000, 0));
            Background15 = new Background(Content.Load<Texture2D>("BG_5"), new Vector2(3000, 0));*/
        }

        public void Update(GameTime gameTime)
        {
            #region Background Stuff
            Background00.position.X -= backspeed;
            Background01.position.X -= backspeed;
            Background10.position.X -= frontspeed;
            Background11.position.X -= frontspeed;

            if (Background00.position.X + Background00.bounds.Width < 1280)
            {
                Background01.position.X = Background00.position.X + Background00.bounds.Width;
            }
            if (Background01.position.X + Background01.bounds.Width < 1280)
            {
                Background00.position.X = Background01.position.X + Background01.bounds.Width;
            }

            if (Background10.position.X + Background10.bounds.Width < 1280)
            {
                Background11.position.X = Background10.position.X + Background10.bounds.Width;
            }
            if (Background11.position.X + Background11.bounds.Width < 1280)
            {
                Background10.position.X = Background11.position.X + Background11.bounds.Width;
            }
            Background00.bounds = new Rectangle((int)Background00.position.X, (int)Background00.position.Y, Background00.texture.Width, Background00.texture.Height);
            Background01.bounds = new Rectangle((int)Background01.position.X, (int)Background01.position.Y, Background01.texture.Width, Background01.texture.Height);
            Background10.bounds = new Rectangle((int)Background10.position.X, (int)Background10.position.Y, Background10.texture.Width, Background10.texture.Height);
            Background11.bounds = new Rectangle((int)Background11.position.X, (int)Background11.position.Y, Background11.texture.Width, Background11.texture.Height);
            #endregion
        }
        
        public void Draw(SpriteBatch spriteBatch, Color c)
        {
            Background00.Draw(spriteBatch,c);
            Background01.Draw(spriteBatch,c);
            Background10.Draw(spriteBatch, c);
            Background11.Draw(spriteBatch, c);
        }
    }
}

