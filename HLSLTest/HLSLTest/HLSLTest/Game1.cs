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


namespace HLSLTest
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }

        Texture2D npc, bg;
        Rectangle npcR;
        Color[] npcC;

        SpriteFont font;

        Effect grayscale;
        bool shader = false;
        
        int scale;

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            npc = Content.Load<Texture2D>("npc");
            bg = Content.Load<Texture2D>("m");

            scale = (graphics.PreferredBackBufferHeight / npc.Height); 
            
            npcR = new Rectangle(0, 0, npc.Width * scale, npc.Height * scale);
            grayscale = Content.Load<Effect>("grayscale");
            font = Content.Load<SpriteFont>("font");

            npcC = new Color[npc.Width * npc.Height];
            npc.GetData(npcC);
            for (int i = 0; i < npcC.Length; i++)
            {
                if (npcC[i].G > 160 || npcC[i].B > 160)
                {
                    //npcC[i].A = 0;
                    
                }
            }
            npc.SetData(npcC);
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        bool flipSwitch(bool a)
        {
            return (a == true)?a = false:a = true;
        }

        bool keyDown = false;
        KeyboardState ks;
        MouseState ms;
        Rectangle m = new Rectangle(0, 0, 0, 0);
        
        string col = "";
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();

            if (ks.IsKeyDown(Keys.Escape)) this.Exit();

            #region lolo
            if (ks.IsKeyDown(Keys.S) && keyDown == false) 
            {
                shader = flipSwitch(shader);
                keyDown = true;
            }
            else if(ks.IsKeyUp(Keys.S))
            {
                keyDown = false;
            }
            #endregion

            m.Location = new Point(ms.X, ms.Y);

            if (m.Intersects(npcR))
            {
                int coord = (((ms.Y/scale) * npc.Width) + ms.X/scale);
                col = npcC[coord].ToString();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (shader)
            {
                grayscale.CurrentTechnique.Passes[0].Apply();
            }

            spriteBatch.Draw(bg, new Rectangle(0,0,bg.Width , bg.Height), Color.White);
            //spriteBatch.Draw(npc, npcR, Color.White);
            spriteBatch.DrawString(font, col, new Vector2(0, graphics.PreferredBackBufferHeight-50), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
