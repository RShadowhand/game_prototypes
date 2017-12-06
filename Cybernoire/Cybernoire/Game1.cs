#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Cybernoire
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        #region ContentInit
        Player p;
        #endregion

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            World w1 = new World("Garman's Apartment",GraphicsDevice, Color.DarkBlue, font);
            World w2 = new World("Snowfall Street",GraphicsDevice, Color.Brown, font);
            World w3 = new World("Slums", GraphicsDevice, Color.Brown, font);

            Door door1 = new Door("Snowfall Street", w2, "Garman's Apartment", false, "door", new Vector2(200, graphics.PreferredBackBufferHeight - 74 * 2));
            w1.addObject(door1);

            Container closet = new Container("Wardrobe", new Item("Jacket"), false, "dolap", new Vector2(500, graphics.PreferredBackBufferHeight - 72*2));
            w1.addObject(closet);
            w1.position = 200;

            Door door2 = new Door("Garman's Apartment", w1, "Snowfall Street", false, "door2", new Vector2(123 * 2, 417));
            w2.addObject(door2);
            w2.height = 11;
            Background w2bg = new Background("bg", false, ObjectType.background, "slums2", Vector2.Zero);
            w2bg.ContentLoad(Content);
            w2.WorldObjects["bg"] = w2bg;

            Door door3 = new Door("Slums", w3, "Snowfall Street", false, "tunnel", new Vector2(560, 428));
            w2.addObject(door3);

            w3.height = 11;
            w3.position = 560;
            Background w3bg = new Background("bg", false, ObjectType.background, "slums1", Vector2.Zero);
            w3bg.ContentLoad(Content);

            w3.WorldObjects["bg"] = w3bg;
            Door tunnel = new Door("Snowfall Street", w2, "Slums", false, "tunnel", new Vector2(100, 428));
            w3.addObject(tunnel);
            
            NPC hobo = new NPC("Homeless Man", false, "hobo", new Vector2(650, 428), font, "I need a jacket!", "Thank you!");
            w3.addObject(hobo);


            w1.ContentLoad(Content);
            w2.ContentLoad(Content);
            w3.ContentLoad(Content);


            p = new Player(w2);
            p.ContentLoad(Content);
        }

        protected override void UnloadContent(){}

        KeyboardState ks;
        MouseState ms;
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();

            if (ks.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            p.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            p.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
