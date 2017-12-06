using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace arkanoid
{
    class Beat
    {
        float beatPerSec;
        float speed;
        Rectangle rect;
        public floatRectangle r;
        Texture2D texture;
        float difficulty = 50;

        public Beat(float beatPerSec, Texture2D texture)
        {
            this.beatPerSec = beatPerSec;
            this.texture = texture;
            rect = new Rectangle(50, 450, 2, 75);
            r = new floatRectangle(-10, 600 - 100, 2, 75);
            float secPerBeat = 1000 / beatPerSec;
            speed = 16 / (secPerBeat / difficulty);
        }

        public void Update(GameTime gt)
        {
            r.X += speed;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.Pink);
            //sb.Draw(texture, r.location, Color.Green);
            sb.Draw(texture, r.Position, rect, Color.Green, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }

    class BeatController
    {
        float bpm;
        int beatCount;
        float beatPerSec;
        Texture2D texture;
        public List<Beat> beats = new List<Beat>();

        public BeatController(int bpm, TimeSpan songlength)
        {
            this.bpm = bpm;
            this.beatPerSec = (bpm / 60);
            this.beatCount = (int)(beatPerSec * songlength.TotalSeconds);
        }

        public void LoadContent(ContentManager c)
        {
            texture = c.Load<Texture2D>("pixel");
        }

        int curCount = 0;
        int ms;
        public void Update(GameTime gt)
        {
            ms += gt.ElapsedGameTime.Milliseconds;
            if (ms > 1000 / beatPerSec)
            {
                ms = 0;
                if (curCount < beatCount)
                {
                    beats.Add(new Beat(beatPerSec, texture));
                    curCount++;
                }
            }


            foreach (Beat b in beats)
            {
                b.Update(gt);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Beat b in beats)
            {
                b.Draw(sb);
            }
        }
    }
}
