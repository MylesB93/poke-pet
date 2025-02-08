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
			string applicationFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			Directory.CreateDirectory(applicationFolderPath);
			string databaseFileName = Path.Combine(applicationFolderPath, "pokemon.db");			

			_connection = new SQLiteAsyncConnection(databaseFileName);
			_connection.CreateTableAsync<Pokemon>();
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
