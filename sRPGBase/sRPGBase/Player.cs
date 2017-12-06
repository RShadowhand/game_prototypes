using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Player : Actor
    {
        public Player(string Name, Point pos, Dictionary<Item, int> inventory, Alignment alignment) :
            base(Name, pos, inventory, alignment)
        {

        }

        public override void Update(GameTime gT)
        {
            base.Update(gT);
        }

        public string getInfo() {
            string Auras = "";
            foreach (Aura a in auras)
            {
                Auras += a.Name.ToString() + ", " + a.timer.ToString();
            }
            return Auras;
        }
    }
}
