using PokePet.Core;
using PokePet.Core.Interfaces;
using System.Globalization;

namespace PokePet
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IPokemonService _pokeService;
		private readonly TextInfo textInfo;

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
                var pokemon = await _pokeService.GetPokemonAsync(entry.Text);
                if (pokemon != null)
                {
					resultLabel.Text = "So you choose " + textInfo.ToTitleCase(pokemon.Name) + " as your companion?";
                    accept.IsVisible = true;
                    cancel.IsVisible = true;
                    entry.IsEnabled = false;
				}					
            }
        }

        private void OnAcceptButtonClicked(object sender, EventArgs e)
		{
			// TODO: Save the selected Pokemon
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
