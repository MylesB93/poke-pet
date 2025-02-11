namespace PokePet;

public partial class PokePet : ContentPage
{
	public PokePet()
	{
		InitializeComponent();
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		//if (Shell.Current.CurrentState.Location.Query.TryGetValue("pid", out var query))
		//{
		//	// Do something
		//}
	}
}