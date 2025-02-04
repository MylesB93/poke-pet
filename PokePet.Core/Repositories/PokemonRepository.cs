using PokePet.Core.Interfaces;
using PokePet.Core.Models;
using SQLite;

namespace PokePet.Core.Repositories
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly SQLiteAsyncConnection _connection;

		public PokemonRepository()
		{
			_connection = new SQLiteAsyncConnection("pokemon-db");
		}

		public async Task SetPokemonAsync(Pokemon pokemon)
		{
			await _connection.CreateTableAsync<Pokemon>();
			await _connection.InsertAsync(pokemon);
		}

		public async Task<List<Pokemon>> GetAllAsync()
		{
			return await _connection.Table<Pokemon>().ToListAsync();
		}
	}
}
