namespace testapp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
            test.SendClicked();
        }
    }

}
