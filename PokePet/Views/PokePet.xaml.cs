using PokePet.Core.Enums;
using PokePet.Core.Features;
using PokePet.Core.Interfaces;
using PokePet.Core.Models;

namespace PokePet.Views;

[QueryProperty(nameof(PokemonId), "pid")]
public partial class PokePet : ContentPage
{
	public int PokemonId { get; set; }
	private IPokemonService _pokeService;
	private Pokemon? _pokemon;

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

		if (_pokemon != null)
		{
			_pokemon.SaveState();
			_pokeService.UpdatePokemonAsync(_pokemon);
		}
	}

	private async void FeedButton_Clicked(object sender, EventArgs e)
	{
		if (_pokemon != null)
		{
			if (_pokemon.CanFeed())
			{
				_pokemon.Feed();
				await _pokeService.UpdatePokemonAsync(_pokemon);
			}
			else if (_pokemon.Hunger == Hunger.Full)
			{
				await DisplayAlert("Warning", "Your Pokémon is not hungry", "OK");
			}
			else
			{
				await DisplayAlert("Warning", $"You need to wait {_pokemon.TimeUntilNextFeed} to feed your Pokémon", "OK");
			}
		}
	}

	private async void SleepButton_Clicked(object sender, EventArgs e)
	{
		if (_pokemon != null)
		{
			if (_pokemon.CanSleep())
			{
				_pokemon.Sleep();
				await _pokeService.UpdatePokemonAsync(_pokemon);
			}
			else if (_pokemon.Tiredness == Tiredness.Awake)
			{
				await DisplayAlert("Warning", "Your Pokémon is not tired", "OK");
			}
			else
			{
				await DisplayAlert("Warning", $"You need to wait {_pokemon.TimeUntilNextSleep} to put your Pokémon to sleep", "OK");
			}
		}
	}

	private async void PlayButton_Clicked(object sender, EventArgs e)
	{
		if (_pokemon != null)
		{
			if (_pokemon.CanPlay())
			{
				_pokemon.Play();
				await _pokeService.UpdatePokemonAsync(_pokemon);
			}
			else if (_pokemon.Boredom == Boredom.NotBored)
			{
				await DisplayAlert("Warning", "Your Pokémon is not bored", "OK");
			}
			else
			{
				await DisplayAlert("Warning", $"You need to wait {_pokemon.TimeUntilNextPlay} to play with your Pokémon", "OK");
			}
		}
	}

	private void OnNicknameEntered(object sender, EventArgs e)
	{
		if (sender is Entry entry && _pokemon != null)
		{
			_pokemon.Nickname = entry.Text.ToLower();
		}
	}
}