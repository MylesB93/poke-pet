using System.Text.Json.Serialization;

namespace PokePet.Core.Models
{
	// All the code in this file is included in all platforms.
	public class Pokemon
	{
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
