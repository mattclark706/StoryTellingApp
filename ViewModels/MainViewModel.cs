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

        private string displayText = "The beginning of the adventure, choose a direction";
        private string button1Text = "Mountain Path";
        private string button2Text = "Forest Path";
        private string button3Text = "Desert Path";
        private bool test = true;


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

        public bool Test
        {
            get => test;
            set
            {
                test = value;
                OnPropertyChanged(nameof(Test));
            }
        }

        private void StartButtonFunction()
        {
            run(0);
            newScenario();
        }
        private void Button1Function()
        {
            run(1);
            newScenario();
        }
        private void Button2Function()
        {
            run(2);
            newScenario();
        }
        private void Button3Function()
        {
            run(3);  
            newScenario();
        }

        public void run(int choice)
        {
            if (scenario.Action == ScenarioAction.Start)
            {
                Button1Text = "Choice 1";
                Button2Text = "Choice 2";
                Button3Text = "Choice 3";
            }
            else if (scenario.Action == ScenarioAction.Fight)
            {
                ifFight(choice);
            }
            else if (scenario.Action == ScenarioAction.Search)
            {
                ifSearch(choice);
            }
        }

        public async void ifFight(int choice)
        {
            if (choice == 1)
            {
                // reduce player health based on monster attack
                await App.Current.MainPage.DisplayAlert("Alert", "You defeat the beast", "OK");
                //player.health -= FightScenario.AnimalDamage;
            }
            else if (choice == 2)
            {
                // player flees
                await App.Current.MainPage.DisplayAlert("Alert", "You flee from the beast", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "This button needs functionality", "OK");
                //DisplayText = "Button 3 Clicked";
            }
        }

        public async void ifSearch(int choice)
        {
            if (choice == 1)
            {
                // reduce player health based on monster attack
                await App.Current.MainPage.DisplayAlert("Alert", "You searched", "OK");
                //player.health -= FightScenario.AnimalDamage;
            }
            else if (choice == 2)
            {
                // player flees
                await App.Current.MainPage.DisplayAlert("Alert", "You searched again", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You Searched for a third time", "OK");
                //DisplayText = "Button 3 Clicked";
            }
        }
        public void newScenario()
        {
            scenario = ScenarioService.randomScenario();
            if (scenario.Action == ScenarioAction.Fight)
            {
                DisplayText = scenario.Description;
                Test = false;
            }
            else if (scenario.Action == ScenarioAction.Search)
            {
                DisplayText = scenario.Description;
                Test = true;
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
