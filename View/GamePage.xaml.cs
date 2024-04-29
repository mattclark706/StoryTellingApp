namespace testapp;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

    private void OnPhotoBtnClicked(object sender, EventArgs e)
    {
		PhotoBtn.IsVisible = false;
    }
}