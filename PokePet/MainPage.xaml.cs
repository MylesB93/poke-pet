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
    }

}
