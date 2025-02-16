using PokePet.Core.Interfaces;
using PokePet.Core.Models;
using System.Globalization;

namespace PokePet.Views;

public partial class Search : ContentPage
    {
        private readonly IPokemonService _pokeService;
	private readonly TextInfo textInfo;
        private Pokemon _selectedPokemon;

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
			await _pokeService.SetPokemonAsync(_selectedPokemon);
			await Shell.Current.GoToAsync($"PokePet?pid={_selectedPokemon.PokemonId}");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}			
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
