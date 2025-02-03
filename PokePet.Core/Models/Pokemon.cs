using SQLite;
using System.Text.Json.Serialization;

namespace PokePet.Core.Models
{
	[Table("pokemon")]
	public class Pokemon
	{
		[PrimaryKey, AutoIncrement]
		public int PokemonId { get; set; } // TODO: figure out how to make this auto increment (currently being initialised as 0)
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("name")]
		public string? Name { get; set; }
		[JsonPropertyName("is_baby")]
		public bool IsBaby { get; set; }
	}

	public class PokemonResponse
	{
		[JsonPropertyName("babies")]
		public List<Pokemon>? Babies { get; set; }
	}
}
