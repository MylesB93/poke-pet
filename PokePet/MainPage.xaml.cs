using PokePet.Core;
using PokePet.Core.Interfaces;
using PokePet.Core.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PokePet
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IPokemonService _pokeService;
		private readonly TextInfo textInfo;
        private Pokemon _selectedPokemon;
		private ObservableCollection<Pokemon> PokemonList { get; set; } = new();

		public MainPage(IPokemonService pokeService)
        {
			_pokeService = pokeService;
			textInfo = CultureInfo.CurrentCulture.TextInfo;

			InitializeComponent();

			BindingContext = this;
			LoadPokemon();
		}

		private async void LoadPokemon() // TODO: Duplicate code
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
			var pokemon = await _pokeService.GetAllPokemonFromDbAsync();

			if (pokemon != null)
			{
				PokemonList.Clear();
				foreach (var mon in pokemon)
				{
					PokemonList.Add(mon);
				}
			}
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
