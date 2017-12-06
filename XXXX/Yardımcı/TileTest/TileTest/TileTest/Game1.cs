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
using System.Collections;

namespace TileTest
{
    public class tile {
        public Texture2D texture;
        public Vector2 loc;
        Rectangle rect;

        public tile(Texture2D texture, Vector2 loc)
        {
            this.texture = texture;
            this.loc = loc;
            rect = new Rectangle((int)loc.X, (int)loc.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, rect, Color.White); 
        }
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D zero, one, two;

        static int arrX, arrY;

        int timeIndex = 0;

        int[,] array;
        tile[,] tiles;

        int posX = 0;
        int posY = 0;

        Random r = new Random();

        List<int[,]> arrays = new List<int[,]>();

        bool KeyDown = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            arrX = (Window.ClientBounds.Width / 16)+1;
            arrY = (Window.ClientBounds.Height / 16)+1;

            tiles = new tile[arrX, arrY];
            array = new int[arrX, arrY];
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight += 20;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();

            zero = Content.Load<Texture2D>("zero");
            one = Content.Load<Texture2D>("one");
            two = Content.Load<Texture2D>("two");

            for (int i = 0; i < arrX; i++)
            {
                for (int j = 0; j < arrY; j++)
                {
                    array[i, j] = r.Next(0, 3);
                }
            }
            arrays.Add(array);
            for (int i = 0; i < arrX; i++)
            {
                for (int j = 0; j < arrY; j++)
                {
                    if (array[i, j] == 0)
                    {
                        tiles[i, j] = new tile(zero, new Vector2(posX, posY));
                    }
                    else if (array[i, j] == 1)
                    {
                        tiles[i, j] = new tile(one, new Vector2(posX, posY));
                    }
                    else if (array[i, j] == 2)
                    {
                        tiles[i, j] = new tile(two, new Vector2(posX, posY));
                    }
                    posY += 16;
                }
                posY = 0;
                posX += 16;
            }
        }

        protected override void UnloadContent(){}

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && KeyDown == false)
            {
                array = new int[arrX,arrY];
                for (int i = 0; i < arrX; i++)
                {
                    for (int j = 0; j < arrY; j++)
                    {
                        array[i, j] = r.Next(0, 3);
                    }
                }
                arrays.Add(array);

                tiles = makeTiles(array);
                timeIndex++;
                KeyDown = true;
            }
            else if (ks.IsKeyDown(Keys.Left) && KeyDown == false)
            {
                if (timeIndex > 0)
                {
                    timeIndex--;
                }
                tiles = makeTiles(arrays[timeIndex]);
                KeyDown = true;
            }
            else if (ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Left))
            {
                KeyDown = false;
            }

            if (ks.IsKeyDown(Keys.A))
            {
                System.Windows.Forms.MessageBox.Show((timeIndex+1).ToString() + " of " + arrays.Count.ToString());
            }
            base.Update(gameTime);
        }

        tile[,] makeTiles(int[,] arr) {
            tile[,] tileset = new tile[arrX, arrY];
            int posX = 0;
            int posY = 0;
            for (int i = 0; i < arrX; i++)
            {
                for (int j = 0; j < arrY; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        tileset[i, j] = new tile(zero, new Vector2(posX, posY));
                    }
                    else if (arr[i, j] == 1)
                    {
                        tileset[i, j] = new tile(one, new Vector2(posX, posY));
                    }
                    else if (arr[i, j] == 2)
                    {
                        tileset[i, j] = new tile(two, new Vector2(posX, posY));
                    }
                    posY += 16;
                }
                posY = 0;
                posX += 16;
            }
            return tileset;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int i = 0; i < arrX; i++)
            {
                for (int j = 0; j < arrY; j++)
                {
                    tiles[i, j].Draw(spriteBatch);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
