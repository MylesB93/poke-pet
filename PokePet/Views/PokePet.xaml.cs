using PokePet.Core.Interfaces;

namespace PokePet.Views;

[QueryProperty(nameof(PokemonId), "pid")]
public partial class PokePet : ContentPage
{
	public int PokemonId { get; set; }
	private IPokemonService _pokeService;

	public PokePet(IPokemonService pokeService)
	{
		InitializeComponent();
		_pokeService = pokeService;
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		BindingContext = await _pokeService.GetSinglePokemonFromDbAsync(PokemonId);
	}
}