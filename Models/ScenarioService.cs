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
        public static StartingScenario start = new StartingScenario();

        //Fight Scenarios
        public static Scenario Eagle = new FightScenario("Fight Eagle", "Eagle", 20, 50);
        public static Scenario Bear = new FightScenario("Fight Bear", "Bear", 15, 150);
        public static Scenario Wolf = new FightScenario("Fight Wolf", "Wolf", 10, 100);

        //Monster List
        static List<Scenario> monsterList = new List<Scenario> { Eagle, Bear, Wolf };

        //Search Scenarios and items
        public static Item MetalArmor = new Armor("MetalArmor", 20);
        public static Scenario Hut = new SearchScenario("Search Hut", "Inside", "Basement", "Garden", MetalArmor);
        public static Item GreatSword = new Weapon("GreatSword", 30);
        public static Scenario Castle = new SearchScenario("Search Castle", "Hall", "Towers", "Cells", GreatSword);
        public static Item Gold = new Gold("Gold", 20);
        public static Scenario Oasis = new SearchScenario("Search Oasis", "Bushes", "Water", "Mud", Gold);

        static List<Scenario> searchList = new List<Scenario> { Hut, Castle, Oasis };

        



        public static Scenario randomScenario()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 3);
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
                return null;
            }
        }

        public static StartingScenario getStartingScenario()
        {
            return start;
        }
    }
}
