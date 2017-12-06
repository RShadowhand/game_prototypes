using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Auras_and_Players
{
    class Player : Actor
    {
        public RevCam r = new RevCam();
        public Player(string Name, Point pos, Dictionary<Item, int> inventory, Alignment alignment) :
            base(Name, pos, inventory, alignment)
        {

        }

        public override void Update(TimeSpan gT)
        {
            System.Windows.Forms.MessageBox.Show(this.Name);
        }
    }
}
