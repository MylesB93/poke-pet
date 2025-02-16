namespace PokePet.Views;

public partial class Start : ContentPage
{
	public Start()
	{
		InitializeComponent();
	}

	public async void OnSearchButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///Search");
	}
}