using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    public enum ScenarioAction
    {
        Fight,
        Search,
        Choice
    }
    public class Scenario
    {
        public ScenarioAction Action { get; set; } = ScenarioAction.Fight;
        public string Description { get; set; }
        public string AnimalName { get; set; }
        public int AnimalDamage { get; set; }
        public int AnimalHealth { get; set; }

        public Scenario(string description, string animalName, int animalDamage, int animalHealth)
        {
            this.Description = description;
            this.AnimalName = animalName;
            this.AnimalDamage = animalDamage;
            this.AnimalHealth = animalHealth;
        }
    }
}
