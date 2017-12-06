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

namespace RecycleGame
{
    public class Recycle
    {
        public Recycle()
        {
           
        }

        GameObjectCollection objects;
        GameObjectCollection trash = new GameObjectCollection();
        List<string> Temiztextures, Kirlitextures;
        Random r = new Random();
        List<Trash> trashes = new List<Trash>();
        Trash.TrashType trashType = Trash.TrashType.rubbish;
        SpriteFont font;
        string EndGame = "";


        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Font1");
            objects = ObjectFactory.MakeCollection("Content/CRecycle", "recycle", Content);
            Temiztextures = TextureLoader.TextureList("Content/recycle/TRecycle");
            Kirlitextures = TextureLoader.TextureList("Content/recycle/KRecycle");
            foreach (string item in Kirlitextures)
            {
                if (item.Contains("sise"))
                {
                    trashType = Trash.TrashType.glass;
                }
                else if (item.Contains("cips") || item.Contains("kagit"))
                {
                    trashType = Trash.TrashType.paper;
                }
                else if (item.Contains("konserve") || item.Contains("kova"))
                {
                    trashType = Trash.TrashType.metal;
                }
                else
                {
                    trashType = Trash.TrashType.rubbish;
                }
                trashes.Add(new Trash(new GameObject(new Vector2(r.Next(0, 600), r.Next(100, 350)), item, "recycle", Content), trashType, Temiztextures));
                trashType = Trash.TrashType.rubbish;
            }
        }
        MouseState mState;
        public void Update(GameTime gameTime)
        {
            mState = Mouse.GetState();
            Rectangle mRect = new Rectangle(mState.X, mState.Y, 1, 1);
            foreach (Trash t in trashes)
            {

                t.Update(gameTime, mState);
                foreach (string key in objects.Items.Keys)
                {
                    if (t.choosen)
                    {
                        if (key.Contains("bin"))
                        {
                            if (mRect.Intersects(objects.Items[key].getRect()))
                            {
                                if (mState.LeftButton == ButtonState.Pressed)
                                {
                                    if (key.Contains(t.type.ToString()))
                                    {
                                        trashes.Remove(t);
                                        goto breakLabel;
                                    }
                                }
                            }
                        }
                    }
                }
                continue;
            breakLabel: break;
            }

            if (trashes.Count < 1)
            {
                EndGame = "Congratulations! You won!";
            }

        }

        public void Draw(SpriteBatch sb)
        {
            objects.Draw(sb);
            foreach (Trash t in trashes)
            {
                t.Draw(sb);
            }
            sb.DrawString(font, EndGame, Vector2.Zero, Color.Black);
        }
    }

    public class Trash {
        public enum TrashType { paper, glass, metal, rubbish }

        public GameObject gob;
        public TrashType type;
        Rectangle mRect;
        List<string> clean;
        public bool choosen = false;
        bool clicked = false;

        public Trash(GameObject gob, TrashType type, List<string> clean) 
        {
            this.gob = gob;
            this.type = type;
            this.clean = clean;
            mRect = new Rectangle(0, 0, 1, 1);
            foreach (string i in clean)
            {
                if (i.Contains(gob.firsttexture + "temiz"))
                {
                    gob.AssignSecondTexture(i);
                }
            }
        }

        public void Update(GameTime gameTime, MouseState mState)
        {
            mRect.Location = new Point(mState.X, mState.Y);

            if (mRect.Intersects(gob.getRect()) && mState.LeftButton == ButtonState.Pressed && clicked == false)
            {
                if (choosen)
                {
                    Deselect();
                }
                else
                {
                    Select();
                }
            }

            if (choosen)
            {
                gob.SwitchToSecond();
            }
            else
            {
                if (mRect.Intersects(gob.getRect()))
                {
                    gob.SwitchToSecond();
                }
                else
                {
                    gob.SwitchToFirst();
                }
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

        public void Select() { choosen = true; }

        public void Deselect() { choosen = false; }

        

        public void Draw(SpriteBatch sb) {
            sb.Draw(gob.rTexture, gob.getRect(), Color.White);
        }
    }
}
