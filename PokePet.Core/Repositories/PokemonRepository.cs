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
			var numberOfPokemon = await _connection.Table<Pokemon>().CountAsync();

			if (numberOfPokemon >= 6)
			{
				throw new Exception("You can only have 6 pokemon at a time.");
			}
			else
			{
				await _connection.InsertAsync(pokemon);
			}
			
		}

		public async Task<List<Pokemon>> GetAllAsync()
		{
			return await _connection.Table<Pokemon>().ToListAsync();
		}

		public async Task DeletePokemonAsync(int pid)
		{
			await _connection.DeleteAsync<Pokemon>(pid);
		}

		public async Task<Pokemon> GetAsync(int pid)
		{
			return await _connection.Table<Pokemon>().Where(p => p.PokemonId == pid).FirstOrDefaultAsync();
		}

		public async Task UpdatePokemonAsync(Pokemon pokemon)
		{
			await _connection.UpdateAsync(pokemon);
		}
	}
}
