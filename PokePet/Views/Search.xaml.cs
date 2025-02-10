using PokePet.Core;
using PokePet.Core.Interfaces;
using PokePet.Core.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PokePet
{
    public partial class Search : ContentPage
    {
        private readonly IPokemonService _pokeService;
		private readonly TextInfo textInfo;
        private Pokemon _selectedPokemon;
		public ObservableCollection<Pokemon> PokemonList { get; set; } = new(); // TODO: Remove this

		public Search(IPokemonService pokeService)
        {
			_pokeService = pokeService;
			textInfo = CultureInfo.CurrentCulture.TextInfo;

			InitializeComponent();

			BindingContext = this;
		}

		private async void OnEntryCompleted(object sender, EventArgs e)
        {
            if (sender is Entry entry)
            {
                _selectedPokemon = await _pokeService.GetPokemonAsync(entry.Text.ToLower());
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
			await Shell.Current.GoToAsync("///PokemonListing");
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
