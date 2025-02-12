using PokePet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokePet.Core.Interfaces
{
	public interface IPokemonService
	{
		Task<Pokemon> GetPokemonAsync(string name);
		Task SetPokemonAsync(Pokemon pokemon);
		Task<List<Pokemon>> GetAllPokemonFromDbAsync();
		Task DeletePokemonAsync(int pid);
		Task<Pokemon> GetSinglePokemonFromDbAsync(int pid);
	}
}
