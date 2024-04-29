using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
        private bool button1Visible = true;
        private bool button2Visible = true;
        private bool button3Visible = true;

        // Variable declaration
        private int gameState = 0;
        private bool gameOver = false;
        private bool shake = false;
        private ImageSource photoImage;
        private ImageSource gameImage = "adventure.jpg";

        // Command Setups
        public ICommand StartButton { get; }
        public ICommand Button1 { get; }
        public ICommand Button2 { get; }
        public ICommand Button3 { get; }
        public ICommand TakePhotoCommand { get; }


        public MainViewModel()
        {
            StartButton = new Command(StartButtonFunction);
            Button1 = new Command(Button1Function);
            Button2 = new Command(Button2Function);
            Button3 = new Command(Button3Function);
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
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
        public bool Button1Visible
        {
            get => button1Visible;
            set
            {
                button1Visible = value;
                OnPropertyChanged(nameof(Button1Visible));
            }
        }
        public bool Button2Visible
        {
            get => button2Visible;
            set
            {
                button2Visible = value;
                OnPropertyChanged(nameof(Button2Visible));
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
        public ImageSource PhotoImage
        {
            get => photoImage;
            set
            {
                photoImage = value;
                OnPropertyChangedPhoto();
            }
        }
        public ImageSource GameImage
        {
            get => gameImage;
            set
            {
                gameImage = value;
                OnPropertyChangedPhoto();
            }
        }

        private async void StartButtonFunction()
        {
            await run(0);
            updatePlayerAttributes();
            newScenario();
        }
        private async void Button1Function()
        {
            await run(1);
            updatePlayerAttributes();
            newScenario();
        }
        private async void Button2Function()
        {
            await run(2);
            updatePlayerAttributes();
            newScenario();
        }
        private async void Button3Function()
        {
            await run(3);
            updatePlayerAttributes();
            newScenario();
        }

        public async Task<Task> run(int choice)
        {
            if (gameOver == false)
            {
                if (scenario.Action == ScenarioAction.Start)
                {
                    // check these
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
                else if (scenario.Action == ScenarioAction.Action)
                {
                    await ifAction(choice, (ActionScenario)scenario);
                }
                else if (scenario.Action == ScenarioAction.Boss)
                {
                    ifBoss(choice, (FinalBoss)scenario);
                }
            }
            else
            {
                reset(choice);
            }
            return Task.CompletedTask;
        }

        public async void ifFight(int choice, FightScenario fightScenario)
        {
            if (choice == 1)
            {
                // reduce player health based on monster attack
                if (player.armor != null)
                {
                    int attack = fightScenario.AnimalDamage - player.armor.Block;
                    player.health -= attack;
                    player.armor = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "You defeat the beast, but he destroys your armor", "OK");
                }
                else
                {
                    player.health -= fightScenario.AnimalDamage;
                    await App.Current.MainPage.DisplayAlert("Alert", "You defeat the beast, but he hurts you in the process", "OK");
                }     
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
                // If item is of equal type, set player item to be that item
                if (searchScenario.Item.GetType() == typeof(Armor))
                {
                    Armor armor = (Armor)searchScenario.Item;
                    player.armor = armor;
                }
                else if (searchScenario.Item.GetType() == typeof(Weapon))
                {
                    Weapon weapon = (Weapon)searchScenario.Item;
                    player.weapon = weapon;
                }
                else if (searchScenario.Item.GetType() == typeof(Gold))
                {
                    Gold gold = (Gold)searchScenario.Item;
                    player.gold += gold.value;
                }
                string temp = "Congrats! You have found " + searchScenario.Item.Name;
                await App.Current.MainPage.DisplayAlert("Alert", temp, "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You find nothing, better luck next time", "OK");
            }
        }

        public async void ifShop(int choice, ShopScenario shopScenario)
        {
            // player can buy item if they have enough gold, value of item = damage/block
            if (choice == 1)
            {            
                if (player.gold >= shopScenario.Armor.Block)
                {
                    player.armor = shopScenario.Armor;
                    player.gold -= shopScenario.Armor.Block;
                    await App.Current.MainPage.DisplayAlert("Alert", "You have purchased new armor!", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "You do not have enough gold, the trader runs away scared as he thinks hes being robbed", "OK");
                }
            }
            else if (choice == 2)
            {
                if (player.gold >= shopScenario.Weapon.Damage)
                {
                    player.weapon = shopScenario.Weapon;
                    player.gold -= shopScenario.Weapon.Damage;
                    await App.Current.MainPage.DisplayAlert("Alert", "You have purchased a new weapon!", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "You do not have enough gold, the trader runs away scared as he thinks hes being robbed", "OK");
                } 
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You leave the trader without buying anything", "OK");
            }
        }

        public async Task ifAction(int choice, ActionScenario scenario)
        {
            Button1Visible = false;
            DisplayText = "That didnt work! Shake your phone to break free!";
            ToggleAccelerometer();
            Accelerometer.ShakeDetected += onShake;
            await Task.Delay(5000);
            if (shake == true)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You break free from the web and continue on your journey", "OK");
            }
            else
            {
                //player.health = 0;
                //gameOver = true;
                //lose();
                await App.Current.MainPage.DisplayAlert("Alert", "The spider eats you", "OK");
            }
            shake = false;
            ToggleAccelerometer();
        }
        public async void ifBoss(int choice, FinalBoss finalBoss)
        {
            if (choice == 1)
            {
                if (player.weapon != null)
                {
                    player.health -= finalBoss.BossDamage;
                    if (player.health > 0)
                    {
                        win();
                        await App.Current.MainPage.DisplayAlert("Alert", "You defeat the dragon and collect the crown! Well done!", "OK");
                    }
                    else
                    {
                        lose();
                        await App.Current.MainPage.DisplayAlert("Alert", "The dragon bests you in combat, unlucky", "OK");
                    }
                }
                else
                {
                    lose();
                    await App.Current.MainPage.DisplayAlert("Alert", "You stupidly attack the dragon without a weapon, he laughs as bites your head off", "OK");
                }   
            }
            else if (choice == 2)
            {
                if (player.armor != null)
                {
                    lose();
                    await App.Current.MainPage.DisplayAlert("Alert", "The sound of your armor banging together woke the dragon, he immedietly removes your head", "OK");
                }
                else
                {
                    win();
                    await App.Current.MainPage.DisplayAlert("Alert", "You successfully snuck past the dragon and retrieved the crown!", "OK");
                }
            }
            else
            {
                lose();
                await App.Current.MainPage.DisplayAlert("Alert", "Really? You fled? After all that? The king removes your head for being such a coward", "OK");
            }
        }

        public void win()
        {
            DisplayText = "Well done! You win!";
            Button1Text = "Play again";
            Button2Text = "Main Menu";
            button3Visible = false;
            gameOver = true;
        }
        public void lose()
        {
            DisplayText = "You died and therefore, you lose";
            Button1Text = "Play again";
            Button2Text = "Main Menu";
            button3Visible = false;
            gameOver = true;
        }

        public void reset(int choice)
        {
            if (choice == 1)
            {
                // somehow play again
            }
            else if (choice == 2)
            {
                //await App.Current.Navigation.PushAsync(new GamePage());
            }
        }
        public void newScenario()
        {
            if (gameState < 10)
            {
                scenario = ScenarioService.randomScenario();
                if (scenario.Action == ScenarioAction.Fight)
                {
                    FightScenario fightScenario = (FightScenario)scenario;
                    if (fightScenario.AnimalName == "Bear")
                    {
                        GameImage = "bear.jpg";
                    }
                    else if (fightScenario.AnimalName == "Eagle")
                    {
                        GameImage = "eagle.jpg";
                    }
                    if (fightScenario.AnimalName == "Wolf")
                    {
                        GameImage = "wolf.jpg";
                    }
                    DisplayText = scenario.Description;
                    Button1Text = "Fight the beast";
                    Button2Text = "Flee with your life";
                    Button1Visible = true;
                    Button2Visible = true;
                    Button3Visible = false;
                    vibrate();
                }
                else if (scenario.Action == ScenarioAction.Search)
                {
                    SearchScenario searchScenario = (SearchScenario)scenario;
                    if (searchScenario.Name == "Hut")
                    {
                        GameImage = "hut.jpg";
                    }
                    else if (searchScenario.Name == "Castle")
                    {
                        GameImage = "castle.jpg";
                    }
                    if (searchScenario.Name == "Oasis")
                    {
                        GameImage = "oasis.jpg";
                    }
                    DisplayText = scenario.Description;
                    Button1Text = searchScenario.Location1;
                    Button2Text = searchScenario.Location2;
                    Button3Text = searchScenario.Location3;
                    Button1Visible = true;
                    Button2Visible = true;
                    Button3Visible = true;
                }
                else if (scenario.Action == ScenarioAction.Shop)
                {
                    ShopScenario shopScenario = (ShopScenario)scenario;
                    if (shopScenario.Type == "Wood")
                    {
                        GameImage = "woodenshop.jpg";
                    }
                    else if (shopScenario.Type == "Stone")
                    {
                        GameImage = "stoneshop.jpg";
                    }
                    if (shopScenario.Type == "Iron")
                    {
                        GameImage = "ironshop.jpg";
                    }
                    DisplayText = scenario.Description;
                    Button1Text = shopScenario.Armor.Name + ": " + shopScenario.Armor.Block.ToString();
                    Button2Text = shopScenario.Weapon.Name + ": "+ shopScenario.Weapon.Damage.ToString();
                    Button3Text = "Dont buy anything";
                    Button1Visible = true;
                    Button2Visible = true;
                    Button3Visible = true;
                }
                else if (scenario.Action == ScenarioAction.Action)
                {
                    ActionScenario actionScenario = (ActionScenario)scenario;
                    GameImage = "spiderweb.jpg";
                    DisplayText = actionScenario.Description;
                    Button1Text = "Click to break free";
                    Button1Visible = true;
                    Button2Visible = false;
                    Button3Visible = false;
                }
            }
            else
            {
                scenario = ScenarioService.getFinalBoss();
                GameImage = "dragon.jpg";
                DisplayText = scenario.Description;
                Button1Text = "Try to fight the dragon";
                Button2Text = "Try to sneak around the dragon";
                Button3Text = "Run away scared";
                Button1Visible = true;
                Button2Visible = true;
                Button3Visible = true;
                vibrate();
            }
            gameState++;
        }
        // check why this works a turn too late
        private void updatePlayerAttributes()
        {
            PlayerHealth = player.health;
            PlayerGold = player.gold;
            if (player.armor != null)
            {
                PlayerArmor = player.armor.Name;
            }
            else
            {
                PlayerArmor = "Empty";
            }
            if (player.weapon != null)
            {
                PlayerWeapon = player.weapon.Name;
            }
            else
            {
                PlayerWeapon = "Empty";
            }
        }
        public void vibrate()
        {
            try
            {
                Vibration.Default.Vibrate(1000);
            }
            catch
            {

            }
        }

        public async Task TakePhotoAsync()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        Stream stream = await photo.OpenReadAsync();
                        PhotoImage = ImageSource.FromStream(() => stream);
                    }
                }
            }
            catch
            {
            }
        }

        public void ToggleAccelerometer()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.Start(SensorSpeed.Game);
                }
                else
                {
                    // Turn off accelerometer
                    Accelerometer.Default.Stop();
                }
            }
        }
        private void onShake(object sender, EventArgs e)
        {
            shake = true;
        }
        protected virtual void OnPropertyChangedPhoto([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
