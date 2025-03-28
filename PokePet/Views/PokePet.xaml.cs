using PokePet.Core.Features;
using PokePet.Core.Interfaces;
using PokePet.Core.Models;

namespace PokePet.Views;

[QueryProperty(nameof(PokemonId), "pid")]
public partial class PokePet : ContentPage
{
	public int PokemonId { get; set; }
	private IPokemonService _pokeService;
	private Pokemon _pokemon;

	public PokePet(IPokemonService pokeService)
	{
		InitializeComponent();
		_pokeService = pokeService;
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		_pokemon = await _pokeService.GetSinglePokemonFromDbAsync(PokemonId);
		_pokemon.LoadLastState();
		BindingContext = _pokemon;		
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();

		_pokemon.SaveState();
		_pokeService.UpdatePokemonAsync(_pokemon);
	}

	private async void FeedButton_Clicked(object sender, EventArgs e)
	{
		if (_pokemon.CanFeed())
		{
			_pokemon.Feed();
			await _pokeService.UpdatePokemonAsync(_pokemon);
		}
		else
		{
			await DisplayAlert("Warning", $"You need to wait {_pokemon.TimeUntilNextFeed} to feed your Pok�mon", "OK");
		}		
	}

	private async void SleepButton_Clicked(object sender, EventArgs e)
	{
		if (_pokemon.CanSleep())
		{
			_pokemon.Sleep();
			await _pokeService.UpdatePokemonAsync(_pokemon);
		}
		else
		{
			await DisplayAlert("Warning", $"You need to wait {_pokemon.TimeUntilNextSleep} to put your Pok�mon to sleep", "OK");
		}		
	}

	private async void PlayButton_Clicked(object sender, EventArgs e)
	{
		if (_pokemon.CanPlay())
		{
			_pokemon.Play();
			await _pokeService.UpdatePokemonAsync(_pokemon);
		}
		else
		{
			await DisplayAlert("Warning", $"You need to wait {_pokemon.TimeUntilNextPlay} to play with your Pok�mon", "OK");
		}
	}
}