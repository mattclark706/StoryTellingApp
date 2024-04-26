using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace testapp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Player player = new Player();
        Scenario scenario = ScenarioService.getStartingScenario();

        // Binding properties
        private string displayText = "The beginning of the adventure, choose a direction";
        private string button1Text = "Mountain Path";
        private string button2Text = "Forest Path";
        private string button3Text = "Desert Path";
        private int playerHealth;
        private int playerGold;
        private string playerArmor;
        private string playerWeapon;
        private bool button3Visible = true;

        private int gameState = 0;
        private bool gameOver = false;

        // Command Setups
        public ICommand StartButton { get; }
        public ICommand Button1 { get; }
        public ICommand Button2 { get; }
        public ICommand Button3 { get; }
        

        public MainViewModel()
        {
            StartButton = new Command(StartButtonFunction);
            Button1 = new Command(Button1Function);
            Button2 = new Command(Button2Function);
            Button3 = new Command(Button3Function);
        }

        public string DisplayText
        {
            get => displayText;
            set
            {
                displayText = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }
        public string Button1Text
        {
            get => button1Text;
            set
            {
                button1Text = value;
                OnPropertyChanged(nameof(Button1Text));
            }
        }
        public string Button2Text
        {
            get => button2Text;
            set
            {
                button2Text = value;
                OnPropertyChanged(nameof(Button2Text));
            }
        }
        public string Button3Text
        {
            get => button3Text;
            set
            {
                button3Text = value;
                OnPropertyChanged(nameof(Button3Text));
            }
        }
        public bool Button3Visible
        {
            get => button3Visible;
            set
            {
                button3Visible = value;
                OnPropertyChanged(nameof(Button3Visible));
            }
        }
        public int PlayerHealth
        {
            get => playerHealth;
            set
            {
                playerHealth = value;
                OnPropertyChanged(nameof(PlayerHealth));
            }
        }
        public int PlayerGold
        {
            get => playerGold;
            set
            {
                playerGold = value;
                OnPropertyChanged(nameof(PlayerGold));
            }
        }
        public string PlayerArmor
        {
            get => playerArmor;
            set
            {
                playerArmor = value;
                OnPropertyChanged(nameof(PlayerArmor));
            }
        }
        public string PlayerWeapon
        {
            get => playerWeapon;
            set
            {
                playerWeapon = value;
                OnPropertyChanged(nameof(PlayerWeapon));
            }
        }

        private void StartButtonFunction()
        {
            run(0);
            newScenario();
            updatePlayerAttributes();
        }
        private void Button1Function()
        {
            run(1);
            newScenario();
            updatePlayerAttributes();
        }
        private void Button2Function()
        {
            run(2);
            newScenario();
            updatePlayerAttributes();
        }
        private void Button3Function()
        {
            run(3);  
            newScenario();
            updatePlayerAttributes();
        }

        public void run(int choice)
        {
            if (gameOver == false)
            {
                if (scenario.Action == ScenarioAction.Start)
                {
                    Button1Text = "Choice 1";
                    Button2Text = "Choice 2";
                    Button3Text = "Choice 3";
                }
                else if (scenario.Action == ScenarioAction.Fight)
                {
                    ifFight(choice, (FightScenario)scenario);
                }
                else if (scenario.Action == ScenarioAction.Search)
                {
                    ifSearch(choice, (SearchScenario)scenario);
                }
                else if (scenario.Action == ScenarioAction.Shop)
                {
                    ifShop(choice, (ShopScenario)scenario);
                }
                else if (scenario.Action == ScenarioAction.Boss)
                {
                    ifBoss(choice, (FinalBoss)scenario);
                }
            }
            else
            {
            }
        }

        public async void ifFight(int choice, FightScenario fightScenario)
        {

            if (choice == 1)
            {
                // reduce player health based on monster attack
                await App.Current.MainPage.DisplayAlert("Alert", "You defeat the beast", "OK");
                player.health -= fightScenario.AnimalDamage;
            }
            else if (choice == 2)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You flee from the beast", "OK");
            }
            else
            {

            }
        }

        public async void ifSearch(int choice, SearchScenario searchScenario)
        {
            if (choice == 1)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You find nothing, better luck next time", "OK");
            }
            else if (choice == 2)
            {
                string temp = "Congrats! You have found " + searchScenario.Item.Name;
                await App.Current.MainPage.DisplayAlert("Alert", temp, "OK");
                // If item is of equal type, set player item to be that item
                if (searchScenario.Item.Equals(typeof(Armor)))
                {
                    Armor armor = (Armor)searchScenario.Item;
                    player.armor = armor;
                }
                else if (searchScenario.Item.Equals(typeof(Weapon)))
                {
                    Weapon weapon = (Weapon)searchScenario.Item;
                    player.weapon = weapon;
                }
                else if (searchScenario.Item.Equals (typeof(Gold)))
                {
                    Gold gold = (Gold)searchScenario.Item;
                    player.gold += gold.value;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You find nothing, better luck next time", "OK");
            }
        }

        public async void ifShop(int choice, ShopScenario shopScenario)
        {
            if (choice == 1)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You have purchased new armor!", "OK");
                player.armor = shopScenario.Armor;
            }
            else if (choice == 2)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You have purchased a new weapon!", "OK");
                player.weapon = shopScenario.Weapon;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You leave the trader without buying anything", "OK");
            }
        }
        public async void ifBoss(int choice, FinalBoss finalBoss)
        {
            if (choice == 1)
            {
                player.health -= finalBoss.BossDamage;
                if (player.health > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "You defeat the dragon!", "OK");
                    win();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "The dragon removes your head", "OK");
                    lose();
                }
                
            }
            else if (choice == 2)
            {
                if (player.armor != null)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "The sound of your armor banging together woke the dragon, he immedietly removes your head", "OK");
                    lose();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "You successfully snuck past the dragon and retrieved the crown!", "OK");
                    win();
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Really? You fled? After all that? The king removes your head for being such a coward", "OK");
                lose();
            }
        }

        public void win()
        {
            DisplayText = "Well done! You win!";
            Button1Text = "Play again";
            Button2Text = "Main Menu";
            gameOver = true;
        }
        public void lose()
        {
            DisplayText = "You died and therefore, you lose";
            Button1Text = "Play again";
            Button2Text = "Main Menu";
            gameOver = true;
        }
        public void newScenario()
        {
            if (gameState < 10)
            {
                scenario = ScenarioService.randomScenario();
                if (scenario.Action == ScenarioAction.Fight)
                {
                    DisplayText = scenario.Description;
                    Button1Text = "Fight the beast";
                    Button2Text = "Flee with your life";
                    Button3Visible = false;
                }
                else if (scenario.Action == ScenarioAction.Search)
                {
                    SearchScenario searchScenario = (SearchScenario)scenario;
                    DisplayText = scenario.Description;
                    Button1Text = searchScenario.Location1;
                    Button2Text = searchScenario.Location2;
                    Button3Text = searchScenario.Location3;
                    Button3Visible = true;
                }
                else if (scenario.Action == ScenarioAction.Shop)
                {
                    ShopScenario shopScenario = (ShopScenario)scenario;
                    DisplayText = scenario.Description;
                    Button1Text = shopScenario.Armor.Name;
                    Button2Text = shopScenario.Weapon.Name;
                    Button3Text = "Dont buy anything";
                    Button3Visible = true;
                }
            }
            else
            {
                scenario = ScenarioService.getFinalBoss();
                DisplayText = scenario.Description;
                Button1Text = "Try to fight the dragon";
                Button2Text = "Try to sneak around the dragon";
                Button3Text = "Run away scared";
                Button3Visible = true;
            }
            gameState++;
        }
        // check why this works a turn too late
        private void updatePlayerAttributes()
        {
            PlayerHealth = player.health;
            PlayerGold = player.gold;
            //PlayerArmor = player.armor.Name ?? "Empty";
            //PlayerWeapon = player.weapon.Name ?? "Empty";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
