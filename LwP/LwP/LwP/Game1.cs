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
using NatureSystem;
using ColorsGame;
using FragilityGame;
using LettersGame;
using MapGame;
using NumbersGame;
using PC;
using RecycleGame;
using ObjectHelper;
using FindCouple;

namespace LwP
{
    public enum GameState
    {
        mainmenu, //Burak
        nature, //Burak
        computer, //Burak
        numbers, //Burak

        findcouple, //Kadriye
        fragility,// Kadriye
        colors, //Kadriye

        recycling, //Esra
        map, //Esra
        letters, //Esra
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        Color backColor = new Color(169, 23, 64);
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        string MenuText = "Choose a Game";

        Texture2D company, logo;
        Rectangle companyRect, logoRect;

        Texture2D[] icons;
        Button[] buttons = new Button[9];

        GameState[] States;

        public Rectangle mRect;

        #region Games
        Nature n = new Nature();
        Colors c = new Colors();
        Recycle r = new Recycle();
        Letters l = new Letters();
        Numbers nu = new Numbers();
        Map map = new Map();
        Fragility f = new Fragility();
        PC.PC p = new PC.PC();
        FindCouple.Class1 fc = new Class1();
        #endregion

        

        public GameState gs = new GameState();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gs = GameState.mainmenu;
            States = new GameState[]{ GameState.nature, GameState.findcouple, GameState.fragility, GameState.computer, GameState.recycling, GameState.map, GameState.letters, GameState.colors, GameState.numbers };
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
        }

        Vector2 baslangic = new Vector2(7, 250);
        Vector2 offset = new Vector2(89, 70);

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            company = Content.Load<Texture2D>("company");
            companyRect = new Rectangle(Window.ClientBounds.Width - company.Width - 5, 5, company.Width, company.Height);

            logo = Content.Load<Texture2D>("logo");
            logoRect = new Rectangle((Window.ClientBounds.Width / 2) - logo.Width / 2, 120, logo.Width, logo.Height);

            icons = new Texture2D[9];
            font = Content.Load<SpriteFont>("Font1");

            #region iconlar
            icons[0] = Content.Load<Texture2D>("icons/nature");
            icons[1] = Content.Load<Texture2D>("icons/esinibul");
            icons[2] = Content.Load<Texture2D>("icons/fragility");
            icons[3] = Content.Load<Texture2D>("icons/pc");
            icons[4] = Content.Load<Texture2D>("icons/recycle");
            icons[5] = Content.Load<Texture2D>("icons/TC");
            icons[6] = Content.Load<Texture2D>("icons/harf");
            icons[7] = Content.Load<Texture2D>("icons/renkler");
            icons[8] = Content.Load<Texture2D>("icons/sayi");
            #endregion

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button(this, new Vector2(baslangic.X + (offset.X * i), baslangic.Y + (offset.Y)), icons[i], States[i]);
            }

            mRect = new Rectangle(0, 0, 1, 1);
            n.LoadContent(Content);
            c.LoadContent(Content);
            r.LoadContent(Content);
            l.LoadContent(Content);
            nu.LoadContent(Content);
            map.LoadContent(Content);
            f.LoadContent(Content);
            p.LoadContent(Content);
            fc.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        KeyboardState kState;
        MouseState mState;

        void MainUpdate(GameTime gameTime, KeyboardState kState, MouseState mState) {
            mRect.X = mState.X;
            mRect.Y = mState.Y;
            
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Update(gameTime,kState, mState);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            kState = Keyboard.GetState();
            mState = Mouse.GetState();
            switch (gs)
            {
                case GameState.mainmenu:
                    MainUpdate(gameTime, kState, mState);
                    break;
                case GameState.nature:
                    n.Update(gameTime);
                    break;
                case GameState.computer:
                    p.Update(gameTime);
                    break;
                case GameState.findcouple:
                    fc.Update(gameTime);
                    break;
                case GameState.fragility:
                    f.Update(gameTime);
                    break;
                case GameState.recycling:
                    r.Update(gameTime);
                    break;
                case GameState.map:
                    map.Update(gameTime);
                    break;
                case GameState.letters:
                    l.Update(gameTime);
                    break;
                case GameState.colors:
                    c.Update(gameTime);
                    break;
                case GameState.numbers:
                    nu.Update(gameTime);
                    break;
                default:
                    gs = GameState.mainmenu;
                    break;
            }
            if (gs != GameState.mainmenu)
            {
                if (kState.IsKeyDown(Keys.Escape))
                {
                    gs = GameState.mainmenu;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);
            spriteBatch.Begin();
            switch (gs)
            {
                case GameState.mainmenu:
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].Draw(spriteBatch);
                    }
                    spriteBatch.DrawString(font, MenuText, new Vector2((Window.ClientBounds.Width / 2) - (font.MeasureString(MenuText).Length() / 2), Window.ClientBounds.Height - 75), Color.OrangeRed);
                    spriteBatch.Draw(company, companyRect, Color.White);
                    spriteBatch.Draw(logo, logoRect, Color.White);
                    break;
                case GameState.nature:
                    n.Draw(spriteBatch);
                    break;
                case GameState.computer:
                    p.Draw(spriteBatch);
                    break;
                case GameState.findcouple:
                    fc.Draw(spriteBatch);
                    break;
                case GameState.fragility:
                    GraphicsDevice.Clear(Color.SkyBlue);
                    f.Draw(spriteBatch);
                    break;
                case GameState.recycling:
                    r.Draw(spriteBatch);
                    break;
                case GameState.map:
                    map.Draw(spriteBatch);
                    break;
                case GameState.letters:
                    l.Draw(spriteBatch);
                    break;
                case GameState.colors:
                    c.Draw(spriteBatch);
                    break;
                case GameState.numbers:
                    nu.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
