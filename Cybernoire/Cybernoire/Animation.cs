using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Cybernoire
{
    class Animation
    {
        float animTimer;
        List<Texture2D> Set = new List<Texture2D>();
        public Texture2D firstFrame;
        public bool animEnded = false;
        float timescale;
        public bool reverse = false;
        public int fixedFrame = -1;
        public bool skipLast = false;

        public Animation(ContentManager Content, string location, int amount, int time, float timescale) 
        {
            for (int i = 1; i <= amount; i++)
            {
                Set.Add(Content.Load<Texture2D>(location + i.ToString()));
            }
            firstFrame = Set[0];
            this.timescale = timescale;
            animTimer = time*timescale;
        }
        public Animation(ContentManager Content, string location, int amount, int time, float timescale, bool reverse)
        {
            for (int i = 1; i <= amount; i++)
            {
                Set.Add(Content.Load<Texture2D>(location + i.ToString()));
            }
            firstFrame = Set[0];
            this.timescale = timescale;
            animTimer = time * timescale;
            this.reverse = reverse;
            if (reverse)
            {
                firstFrame = Set[Set.Count - 1];
                pFrame = Set.Count - 1;
            }
        }

        int Timer = 0;
        int pFrame = 0;
        public Texture2D getFrame(GameTime t)
        {
            if (fixedFrame != -1)
            {
                pFrame = fixedFrame;
            }
            else if (reverse)
            {
                pFrame = getBackFrame(t);
            }
            else
            {
                Timer += t.ElapsedGameTime.Milliseconds;
                if (Timer > animTimer)
                {
                    Timer = 0;
                    pFrame++;
                    if (pFrame > Set.Count - 1)
                    {
                        animEnded = true;
                        if (skipLast)
                        {
                            pFrame = Set.Count - 1;
                        }
                        else
                        {
                            pFrame = 0;
                        }
                    }
                    else
                    {
                        animEnded = false;
                    }
                }
            }
            return Set[pFrame];
        }
        
        int getBackFrame(GameTime t)
        {
            Timer += t.ElapsedGameTime.Milliseconds;
            if (Timer > animTimer)
            {
                Timer = 0;
                pFrame--;
                if (pFrame < 0)
                {
                    animEnded = true;
                    if (skipLast)
                    {
                        pFrame = 0;
                    }
                    else
                    {
                        pFrame = Set.Count - 1;
                    }
                }
                else
                {
                    animEnded = false;
                }
            }
            return pFrame;
        }

        int frameAmount() { 
            return Set.Count;
        }

        public void reset() {
            Timer = 0;
            animEnded = false;
            if (reverse)
            {
                pFrame = Set.Count -1;
            }
            else
            {
                pFrame = 0;
            }
        }
    }
}
