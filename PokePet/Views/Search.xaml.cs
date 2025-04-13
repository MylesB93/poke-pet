using PokePet.Core.Interfaces;
using PokePet.Core.Models;

namespace PokePet.Views;

public partial class Search : ContentPage
{
	private readonly IPokemonService _pokeService;
	private Pokemon? _selectedPokemon;

	public Search(IPokemonService pokeService)
	{
		_pokeService = pokeService;

		InitializeComponent();

		BindingContext = this;
	}

	private async void OnEntryCompleted(object sender, EventArgs e)
	{
		if (sender is Entry entry)
		{
			_selectedPokemon = await _pokeService.GetPokemonAsync(entry.Text.ToLower().Trim());
			if (_selectedPokemon != null && !string.IsNullOrEmpty(_selectedPokemon.Name))
			{
				resultLabel.Text = "So you choose " + _selectedPokemon.Name + " as your companion?";
				accept.IsVisible = true;
				cancel.IsVisible = true;
				entry.IsEnabled = false;
			}
		}
	}

	private async void OnAcceptButtonClicked(object sender, EventArgs e)
	{
		ResetSearch();
		try
		{
			if (_selectedPokemon != null)
			{
				await _pokeService.SetPokemonAsync(_selectedPokemon);
				await Shell.Current.GoToAsync($"PokePet?pid={_selectedPokemon.PokemonId}");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private async void OnLinkTapped(object sender, EventArgs e)
	{
		await Launcher.Default.OpenAsync("https://bulbapedia.bulbagarden.net/wiki/Baby_Pok%C3%A9mon#List_of_baby_Pok%C3%A9mon");
	}

	private void OnCancelButtonClicked(object sender, EventArgs e)
	{
		ResetSearch();
	}

	private void ResetSearch()
	{
		entry.Text = "";
		entry.IsEnabled = true;
		resultLabel.Text = "";
		accept.IsVisible = false;
		cancel.IsVisible = false;
	}
}
