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

namespace FindCouple
{
    public class Class1
    {
        List<string> Textures;
        List<GameObject> Objects = new List<GameObject>();
        int[,] board = new int[4, 4];
        Random r = new Random();
        string textBuffer = "";
        bool clicked = false;

        public void LoadContent(ContentManager Content)
        {
            Textures = TextureLoader.TextureList("Content/findcouple/textures");
            #region board
            int x = 0, y = 0, value = 1;
            while (HasEmptySpot(board, 4, 4))
            {
                value = r.Next(0, 7);
            first:
                {
                    x = r.Next(0, 4);
                    y = r.Next(0, 4);
                    if (board[x, y] == 0)
                    {
                        board[x, y] = value;
                    }
                    else
                    {
                        goto first;
                    }
                }
            second:
                {
                    x = r.Next(0, 4);
                    y = r.Next(0, 4);
                    if (board[x, y] == 0)
                    {
                        board[x, y] = value;
                    }
                    else
                    {
                        goto second;
                    }
                }
            }
            #endregion
            #region tiles
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 1)
                    {
                        textBuffer = "bilgisayar";
                    }
                    else if (board[i, j] == 2)
                    {
                        textBuffer = "fare";
                    }
                    else if (board[i, j] == 3)
                    {
                        textBuffer = "hoparlor";
                    }
                    else if (board[i, j] == 4)
                    {
                        textBuffer = "televizyon";
                    }
                    else if (board[i, j] == 5)
                    {
                        textBuffer = "tablet";
                    }
                    else if (board[i, j] == 6)
                    {
                        textBuffer = "telefon";
                    }

                    Objects.Add(new GameObject(new Vector2(((i + 2) * 107) - 53, j * 115), textBuffer, "findcouple", Content));
                }
            }
            #endregion
            foreach (GameObject g in Objects)
            {
                g.AssignSecondTexture("kapali");
                g.SwitchToSecond();
            }
        }

        public bool HasEmptySpot(int[,] a, int x, int y) {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (a[i,j] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        MouseState mState;
        Rectangle mRect = new Rectangle(0,0,1,1);
        List<GameObject> selected = new List<GameObject>();

        public void Update(GameTime gameTime)
        {
            mState = Mouse.GetState();
            mRect.Location = new Point(mState.X, mState.Y);
            foreach (GameObject g in Objects)
            {
                if (mRect.Intersects(g.getRect()) && mState.LeftButton == ButtonState.Pressed && clicked == false)
                {
                    if (g.Selected)
                    {
                        g.Deselect();
                        g.SwitchTexture();
                        selected.RemoveAt(selected.IndexOf(g));
                    }
                    else
                    {
                        g.Select();
                        g.SwitchTexture();
                        selected.Add(g);
                    }
                }
            }

            if (selected.Count > 2)
            {
                if (selected[1].firsttexture == selected[0].firsttexture)
                {
                    Objects.RemoveAt(Objects.IndexOf(selected[0]));
                    Objects.RemoveAt(Objects.IndexOf(selected[1]));
                }
                foreach (GameObject g in Objects)
                {
                    g.Deselect();
                    g.SwitchToSecond();
                }
                selected.Clear();
            }



            if (mState.LeftButton == ButtonState.Pressed)
            {
                clicked = true;
            }
            else if (mState.LeftButton == ButtonState.Released)
            {
                clicked = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject g in Objects)
            {
                g.Draw(sb);
            }
        }
    }
}
