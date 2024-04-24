using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    public static class ScenarioService
    {
        public static Scenario Eagle = new Scenario("Fight Eagle", "Eagle", 20, 50);
        public static Scenario Bear = new Scenario("Fight Bear", "Bear", 15, 150);
        public static Scenario Wolf = new Scenario("Fight Wolf", "Wolf", 10, 100);
        static List<Scenario> monsterList = new List<Scenario> {Eagle, Bear, Wolf};

        public static Scenario randomScenario()
        {
            int num = monsterList.Count;
            Random rnd = new Random();
            num = rnd.Next(0, num);
            return monsterList[num];
        }
    }
}
