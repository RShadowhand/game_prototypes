using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Auras_and_Players;

namespace Auras_and_Players
{

    enum Alignment { good, evil, }
    public enum Stat { agility, strength, intellect, health, mana, xp, AttribPts, money, level, }

    class Actor
    {
        public string Name;
        public List<Aura> auras = new List<Aura>();
        public Dictionary<Stat, int> actorStats;
        //Texture2D texture;
        Point pos; //vector2
        Dictionary<Item, int> inventory;
        Alignment alignment;
        public AuraHandler auraHandler;

        protected Actor(string Name, Point pos, Dictionary<Item, int> inventory, Alignment alignment)
        {
            this.Name = Name;
            this.pos = pos;
            this.inventory = inventory;
            this.alignment = alignment;
            actorStats = new Dictionary<Stat, int>();
            actorStats.Add(Stat.agility, 3);
            actorStats.Add(Stat.strength, 3);
            actorStats.Add(Stat.intellect, 3);
            actorStats.Add(Stat.health, 100);
            actorStats.Add(Stat.mana, 100);
            actorStats.Add(Stat.xp, 0);
            actorStats.Add(Stat.money, 0);
            actorStats.Add(Stat.AttribPts, 0);
            actorStats.Add(Stat.level, 0);
        }

        public virtual void Update(TimeSpan gT) {
            foreach (Aura a in auras)
            {
                a.Update();
            }
        }

        public int indexOf(string name)
        {
            for (int i = 0; i < auras.Count; i++)
            {
                if (auras[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public void AddAura(Aura aura)
        {
            aura.addAura(this);
        }

        public void DispellAura(Aura aura)
        {
            aura.removeAura(this);
        }
    }
}
