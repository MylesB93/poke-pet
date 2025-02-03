using PokePet.Core;
using PokePet.Core.Interfaces;
using PokePet.Core.Models;
using System.Globalization;

namespace PokePet
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IPokemonService _pokeService;
		private readonly TextInfo textInfo;
        private Pokemon _selectedPokemon;

		public MainPage(IPokemonService pokeService)
        {
			_pokeService = pokeService;
			textInfo = CultureInfo.CurrentCulture.TextInfo;

			InitializeComponent();
        }

        private async void OnEntryCompleted(object sender, EventArgs e)
        {
            if (sender is Entry entry)
            {
                _selectedPokemon = await _pokeService.GetPokemonAsync(entry.Text);
                if (_selectedPokemon != null && !string.IsNullOrEmpty(_selectedPokemon.Name))
                {
					resultLabel.Text = "So you choose " + textInfo.ToTitleCase(_selectedPokemon.Name) + " as your companion?";
                    accept.IsVisible = true;
                    cancel.IsVisible = true;
                    entry.IsEnabled = false;
				}					
            }
        }

        private async void OnAcceptButtonClicked(object sender, EventArgs e)
		{
            await _pokeService.SetPokemonAsync(_selectedPokemon);
		}

		private void OnCancelButtonClicked(object sender, EventArgs e)
        { 
            entry.IsEnabled = true;
			resultLabel.Text = "";
			accept.IsVisible = false;
			cancel.IsVisible = false;
		}
	}
}
