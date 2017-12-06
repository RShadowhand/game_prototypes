using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auras_and_Players
{
    class AuraHandler
    {
        Dictionary<string, Actor> Actors;

        public AuraHandler(Dictionary<string, Actor> Actors)
        {
            this.Actors = Actors;
        }

        public void Update() {

        }

        public void AddAura(Aura aura, string Target)
        {
            aura.addAura(Actors[Target]);
        }

        public void DispellAura(Aura aura, string Target)
        {
            aura.removeAura(Actors[Target]);
        }
    }
}
