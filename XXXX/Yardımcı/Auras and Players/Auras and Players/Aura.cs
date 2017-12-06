using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Auras_and_Players
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
        int time;

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
                time = 0;
                //timer.Elapsed += timer_Elapsed;
                t.actorStats[stat] += stacks;
                //timer.Enabled = true;
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

        TimeSpan timer = new TimeSpan();
        public void Update()
        {
            timer.Add(new TimeSpan(0, 0, 0, 0, 16));
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (time >= seconds)
            {
                if (type == Type.buff)
                {
                    target.actorStats[stat] -= stacks;
                    target.auras.Remove(this);
                    //timer.Enabled = false;
                    onRanOut(e);
                }
            }
            time++;
        }
    }
}
