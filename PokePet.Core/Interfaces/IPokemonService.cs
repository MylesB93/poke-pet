using PokePet.Core.Models;

namespace PokePet.Core.Interfaces
{
	public interface IPokemonService
	{
		Task<Pokemon?> GetPokemonAsync(string name);
		Task SetPokemonAsync(Pokemon pokemon);
		Task<List<Pokemon>> GetAllPokemonFromDbAsync();
		Task DeletePokemonAsync(int pid);
		Task<Pokemon> GetSinglePokemonFromDbAsync(int pid);
		Task UpdatePokemonAsync(Pokemon pokemon);
	}
}
