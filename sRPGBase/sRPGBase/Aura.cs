using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace sRPGBase
{

    public delegate void ranOut(object sender, EventArgs e);

    class Aura
    {
        public enum Type { stat, buff, } //damager
        public int seconds;
        Stat stat = new Stat();
        int stacks;
        public string Name;
        public Actor target;
        Type type;

        //Timer timer = new Timer(1000);

        public event ranOut buffRanOut;
        protected virtual void onRanOut(EventArgs e)
        {
            if (buffRanOut != null)
                buffRanOut(this, e);
        }


        public Aura(int seconds, Stat stat, int stacks, string Name, Actor target)
        {
            this.seconds = seconds;
            this.stat = stat;
            this.stacks = stacks;
            this.Name = Name;
            this.target = target;
            Initiliaze();
        }

        public Aura(Stat stat, int stacks, string Name, Actor target)
        {
            this.stat = stat;
            this.stacks = stacks;
            this.Name = Name;
            this.target = target;
            Initiliaze();
        }

        void Initiliaze()
        {
            if (seconds == 0)
                 { this.type = Type.stat; }
            else { this.type = Type.buff; }
        }

        public void addAura(Actor t) 
        {
            if (t == null)
            {
                t = this.target;
            }
            if (type == Type.buff)
            {
                timer = 0;
                t.actorStats[stat] += stacks;
            }
            if (type == Type.stat)
            {
                t.actorStats[stat] += stacks;
            }
            t.auras.Add(this);
        }

        public void removeAura(Actor t) {
            if (t == null)
            {
                t = this.target;
            }
            if (type == Type.buff)
            {
                t.actorStats[stat] -= stacks;
                //timer.Enabled = false;
            }
            else if (type == Type.stat)
            {
                t.actorStats[stat] -= stacks;
            }
            t.auras.Remove(this);
        }

        public int timer = 0;
        public void Update(GameTime gt)
        {
            timer += gt.ElapsedGameTime.Milliseconds;
            if (timer >= seconds*1000)
            {
                if (type == Type.buff)
                {
                    target.actorStats[stat] -= stacks;
                    target.auras.RemoveAt(target.indexOf(this.Name));
                    onRanOut(new EventArgs());
                }
            }
        }
    }
}
