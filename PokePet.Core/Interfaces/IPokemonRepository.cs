using PokePet.Core.Models;

namespace PokePet.Core.Interfaces
{
	public interface IPokemonRepository
	{
		Task SetPokemonAsync(Pokemon pokemon);
		Task<List<Pokemon>> GetAllAsync();
		Task DeletePokemonAsync(int pid);
		Task<Pokemon> GetAsync(int pid);
	}
}
