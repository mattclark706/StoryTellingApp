using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    public static class ScenarioService
    {
        // Starting Scenario
        static StartingScenario start = new StartingScenario();

        // Final Boss
        static FinalBoss Boss = new FinalBoss();

        // Fight Scenarios
        static Scenario Eagle = new FightScenario("You are attacked by an eagle! What will you do?", "Eagle", 20, 50);
        static Scenario Bear = new FightScenario("A bear jumps out from the trees, what should you do?", "Bear", 15, 150);
        static Scenario Wolf = new FightScenario("A wolf sneaks up behind you and pounces, what do you do?", "Wolf", 10, 100);

        // Monster List
        static List<Scenario> monsterList = new List<Scenario> { Eagle, Bear, Wolf };

        // Items
        static Armor WoodenArmor = new Armor("WoodArmor", 10);
        static Weapon WoodenSword = new Weapon("WoodenSword", 10);
        static Armor StoneArmor = new Armor("StoneArmor", 15);
        static Weapon StoneSword = new Weapon("StoneSword", 15);
        static Armor IronArmor = new Armor("IronArmor", 20);
        static Weapon IronSword = new Weapon("IronSword", 30);
        static Item Gold = new Gold("Gold", 20);

        // Item List
        static List<Item> itemList = new List<Item> { WoodenArmor, WoodenSword, StoneArmor, StoneSword, IronArmor, IronSword };

        // Search Scenarios

        static Scenario Hut = new SearchScenario("Search Hut", "Hut", "Inside", "Basement", "Garden", IronArmor);
        static Scenario Castle = new SearchScenario("Search Castle", "Castle", "Hall", "Towers", "Cells", IronSword);
        static Scenario Oasis = new SearchScenario("Search Oasis", "Oasis", "Bushes", "Water", "Mud", Gold);

        // Search Scenario List
        static List<Scenario> searchList = new List<Scenario> { Hut, Castle, Oasis };

        // Shop Scenarios
        static Scenario WoodShop = new ShopScenario(WoodenArmor, WoodenSword, "Wood", "You have come across a traveller selling wooden items, you may purchase an item if you have the coin");
        static Scenario StoneShop = new ShopScenario(StoneArmor, StoneSword, "Stone", "You have come across a mason selling stone items, you may purchase an item if you have the coin");
        static Scenario IronShop = new ShopScenario(IronArmor, IronSword, "Iron", "You have come across a smith selling iron items, you may purchase an item if you have the coin");

        // Shop List
        static List<Scenario> shopList = new List<Scenario> { WoodShop, StoneShop, IronShop };

        // Action Scenarios
        static Scenario Spider = new ActionScenario("You are caught in a spiders web");

        //Action List
        static List<Scenario> actionList = new List<Scenario> { Spider };

        //Return a random scenario from any of the 3 lists
        public static Scenario randomScenario()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 5);
            if (num == 1)
            {
                num = monsterList.Count;
                num = rnd.Next(0, num);
                return monsterList[num];
            }
            else if (num == 2)
            {
                num = searchList.Count;
                num = rnd.Next(0, num);
                return searchList[num];
            }
            else if (num == 3)
            {
                num = shopList.Count;
                num = rnd.Next(0, num);
                return shopList[num];
            }
            else if (num == 4)
            {
                num = actionList.Count;
                num = rnd.Next(0, num);
                return actionList[num];
            }
            else
            {
                return null;
            }
        }

        public static Scenario getFinalBoss()
        {
            return Boss;
        }
        public static StartingScenario getStartingScenario()
        {
            return start;
        }
    }
}
