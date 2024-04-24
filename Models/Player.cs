using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    public class Player
    {
        public string name { get; set; }
        public int strength { get; set; } = 10;
        public int health { get; set; } = 100;
        public int gold { get; set; } = 10;
        public Weapon weapon { get; set; } = null;
        public Armor armor { get; set; } = null;
    }

    public class Item
    {
        public string Name { get; set; }
    }

    public class Weapon : Item
    {
        public int Damage { get; set; }

        public Weapon(string name, int damage) 
        {
            this.Name = name;
            this.Damage = damage;
        }
    }

    public class Armor : Item
    {
        public int Block { get; set; }
        public Armor(string name, int block)
        {
            this.Name=name;
            this.Block = block;
        }
    }
    public class Gold : Item
    {
        public int value { get; set; }
        public Gold(string name, int value)
        {
            this.Name = name;
            this.value = value;
        }
    }
}
