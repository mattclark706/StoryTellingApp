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

        public ICommand Button1 { get; }
        public ICommand Button2 { get; }
        public ICommand Button3 { get; }
        private string displayText = "No name";
        private string buttonClicked = "No button clicked";
        Scenario scenario = ScenarioService.Bear;
        public bool test;

        public MainViewModel()
        {
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

        public string ButtonClicked
        {
            get => buttonClicked;
            set
            {
                buttonClicked = value;
                OnPropertyChanged(nameof(ButtonClicked));
            }
        }

        // Button hiding not working
        public bool Test
        {
            get => test;
            set
            {
                test = value;
                OnPropertyChanged(nameof(Test));
            }
        }

        public void run(int choice)
        {
            if (scenario.Action == ScenarioAction.Fight)
            {
                ifFight(choice);
            }
        }

        public async void ifFight(int choice)
        {
            if (choice == 1)
            {
                // reduce player health based on monster attack
                await App.Current.MainPage.DisplayAlert("Alert", "You defeat the beast", "OK");
                player.health -= scenario.AnimalDamage;
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

        public void newScenario()
        {
            scenario = ScenarioService.randomScenario();
            if (scenario.Action == ScenarioAction.Fight)
            {
                DisplayText = scenario.Description;
                test = false;
            }
            
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
