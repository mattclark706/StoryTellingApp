namespace testapp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            RequestedThemeChanged += (s, a) =>
            {
                // Logic here if needed to react to theme change
                Console.WriteLine($"Theme changed to: {a.RequestedTheme}");
            };
        }
    }
}
