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

        //Fight Scenarios
        static Scenario Eagle = new FightScenario("Fight Eagle", "Eagle", 20, 50);
        static Scenario Bear = new FightScenario("Fight Bear", "Bear", 15, 150);
        static Scenario Wolf = new FightScenario("Fight Wolf", "Wolf", 10, 100);

        //Monster List
        static List<Scenario> monsterList = new List<Scenario> { Eagle, Bear, Wolf };

        //Items
        static Armor WoodenArmor = new Armor("WoodArmor", 10);
        static Weapon WoodenSword = new Weapon("WoodenSword", 10);
        static Armor StoneArmor = new Armor("StoneArmor", 15);
        static Weapon StoneSword = new Weapon("StoneSword", 15);
        static Armor IronArmor = new Armor("IronArmor", 20);
        static Weapon IronSword = new Weapon("IronSword", 30);
        static Item Gold = new Gold("Gold", 20);

        //Item List
        static List<Item> itemList = new List<Item> { WoodenArmor, WoodenSword, StoneArmor, StoneSword, IronArmor, IronSword };

        //Search Scenarios

        static Scenario Hut = new SearchScenario("Search Hut", "Inside", "Basement", "Garden", IronArmor);
        static Scenario Castle = new SearchScenario("Search Castle", "Hall", "Towers", "Cells", IronSword);
        static Scenario Oasis = new SearchScenario("Search Oasis", "Bushes", "Water", "Mud", Gold);

        //Search Scenario List
        static List<Scenario> searchList = new List<Scenario> { Hut, Castle, Oasis };

        //Shop Scenario
        static Scenario WoodShop = new ShopScenario(WoodenArmor, WoodenSword);
        static Scenario StoneShop = new ShopScenario(StoneArmor, StoneSword);
        static Scenario IronShop = new ShopScenario(IronArmor, IronSword);

        //Shop List
        static List<Scenario> shopList = new List<Scenario> { WoodShop, StoneShop, IronShop };

        //Return a random scenario from any of the 3 lists
        public static Scenario randomScenario()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 4);
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
            else
            {
                num = shopList.Count;
                num = rnd.Next(0, num);
                return shopList[num];
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
