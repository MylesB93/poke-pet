using PokePet.Core;
using PokePet.Core.Interfaces;

namespace PokePet
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IPokemonService _pokeService;

		public MainPage(IPokemonService pokeService)
        {
			_pokeService = pokeService;
			InitializeComponent();
        }

        private async void OnEntryCompleted(object sender, EventArgs e)
        {
            if (sender is Entry entry)
            {
                var pokemon = await _pokeService.GetPokemonAsync(entry.Text);
                if (pokemon != null)
                {
					resultLabel.Text = pokemon.Name;
				}					
            }
        }
    }

}
