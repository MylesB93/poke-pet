using PokePet.Core.Interfaces;
using PokePet.Core.Models;
using System.Collections.ObjectModel;

namespace PokePet;

public partial class PokemonListing : ContentPage
{
	private readonly IPokemonService _pokeService;
	public ObservableCollection<Pokemon> PokemonList { get; set; } = new();

	public PokemonListing(IPokemonService pokeService)
	{
		InitializeComponent();
		_pokeService = pokeService;

		BindingContext = this;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		LoadPokemon();
	}

	private async void OnReleaseButtonClicked(object sender, EventArgs e)
	{
		var button = sender as Button;

		if (button?.CommandParameter is int pid)
		{
			await _pokeService.DeletePokemonAsync(pid);
			LoadPokemon();
		}
	}

	private async void LoadPokemon()
	{
		var pokemonFromDb = await _pokeService.GetAllPokemonFromDbAsync();
		if (pokemonFromDb != null)
		{
			PokemonList.Clear();
			foreach (var pokemon in pokemonFromDb)
			{
				PokemonList.Add(pokemon);
			}
		}
	}
}