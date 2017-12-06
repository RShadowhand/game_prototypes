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


namespace LwP
{
    public class Button : Microsoft.Xna.Framework.GameComponent
    {
        Game1 g;
        Vector2 pos;
        Texture2D disp;
        GameState gState;
        public Rectangle target;

        public Button(Game1 game, Vector2 position, Texture2D display, GameState gamestate) : base(game)
        {
            this.g = game;
            this.pos = position;
            this.disp = display;
            this.gState = gamestate;
            target = new Rectangle((int)pos.X, (int)pos.Y, disp.Width, disp.Height);
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public void Update(GameTime gameTime, KeyboardState kState, MouseState mState) 
        {
            if (g.mRect.Intersects(target))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    Click();
                }
            }

        }
        
        public void Click() {
            //System.Windows.Forms.MessageBox.Show(gState.ToString()); 
            g.gs = gState;
        }

        public void Draw(SpriteBatch sb) 
        {
            sb.Draw(disp, target, Color.White);
        }
    }
}
