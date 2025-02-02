using SQLite;
using System.Text.Json.Serialization;

namespace PokePet.Core.Models
{
	[Table("pokemon")]
	public class Pokemon
	{
		[JsonPropertyName("id")]
		public int PokemonId { get; set; }
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
