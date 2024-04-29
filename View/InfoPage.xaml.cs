namespace testapp.View;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
	}

    private void OnModeClicked(object sender, EventArgs e)
    {
        ToggleThemeButton_Clicked(sender, e);
    }
    private void ToggleThemeButton_Clicked(object sender, EventArgs e)
    {
        var currentTheme = Application.Current.UserAppTheme;
        Application.Current.UserAppTheme = currentTheme == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
    }
}