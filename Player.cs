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

    public class Weapon
    {
        string name;
        int damage;
    }

    public class Armor
    {
        string name;
        int block;
    }
}
