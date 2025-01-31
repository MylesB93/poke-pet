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

        private async void OnCounterClicked(object sender, EventArgs e)
        {
			count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
