using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    public enum ScenarioAction
    {
        Fight,
        Search,
        Shop,
        Boss,
        Start
    }
    public class Scenario
    {
        public ScenarioAction Action { get; set; }
        public string Description { get; set; }


        /*public string AnimalName { get; set; }
        public int AnimalDamage { get; set; }
        public int AnimalHealth { get; set; }

        public Scenario(string description, string animalName, int animalDamage, int animalHealth)
        {
            this.Description = description;
            this.AnimalName = animalName;
            this.AnimalDamage = animalDamage;
            this.AnimalHealth = animalHealth;
        }*/
    }

    public class FightScenario : Scenario
    {
        public string AnimalName { get; set; }
        public int AnimalDamage { get; set; }
        public int AnimalHealth { get; set; }

        public FightScenario(string description, string animalName, int animalDamage, int animalHealth)
        {
            this.Action = ScenarioAction.Fight;
            this.Description=description;
            this.AnimalName = animalName;
            this.AnimalDamage = animalDamage;
            this.AnimalHealth = animalHealth;
        }

        public FightScenario()
        {

        }
    }

    public class SearchScenario : Scenario
    {

        public Item? Item { get; set; } //nullable
        public string Location1;
        public string Location2;
        public string Location3;

        public SearchScenario(string description, string location1, string location2, string location3)
        {
            this.Action = ScenarioAction.Search;
            this.Description=description;
            this.Location1=location1;
            this.Location2=location2;
            this.Location3 =location3;
        }
        public SearchScenario(string description, string location1, string location2, string location3, Item item)
        {
            this.Action = ScenarioAction.Search;
            this.Item = item;
            this.Description=description;
            this.Location1 = location1;
            this.Location2 = location2;
            this.Location3 = location3;
        }
    }

    public class ShopScenario : Scenario
    {
        public Armor Armor;
        public Weapon Weapon;
        public string None;
        public ShopScenario(Armor armor, Weapon weapon)
        {
            this.Action = ScenarioAction.Shop;
            this.Description = "You have come across a traveller selling wares, you may purchase an item if you have the coin";
            this.Armor = armor;
            this.Weapon = weapon;
        }
    }

    public class FinalBoss : Scenario
    {
        public string Name = "Dragon";
        public int BossDamage = 50;
        public int BossHealth = 300;
        public FinalBoss()
        {
            this.Description = "You have found the crown! However, it is guarded by a dragon! You must defeat the dragon to retrieve the crown";
            this.Action = ScenarioAction.Boss;
        }
    }

    public class StartingScenario : Scenario
    {
        public StartingScenario()
        {
            this.Action = ScenarioAction.Start;
        }
    }
}
