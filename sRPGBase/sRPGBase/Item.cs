using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sRPGBase
{
    public enum ItemType { armor, weapon }
    
    public class Item
    {
        string name;
        ItemType type;
        bool equipped = false;
        public Item(string name, ItemType type) {
            this.name = name;
            this.type = type;
        }
    }
    /*
    class ItemEffect : Aura{
        
    }

    class Armor : Item
    {
        public enum Subtype { light, medium, heavy }

        int durability = 50;
        int defense = 10;
        List<ItemEffect> effects = new List<ItemEffect>();

        public Armor(string name, Subtype subtype) : base(name, ItemType.armor) 
        {
            
        }
    }
    
    class Weapon : Item
    {
        public enum Subtype { dagger, sword, bow }
                
        int durability = 10;
        int minDMG = 10;
        int maxDMG = 10;
        List<ItemEffect> effects = new List<ItemEffect>();

        public Weapon(string name, Subtype subtype) : base(name, ItemType.weapon) 
        {
            
        }
    }*/
}
