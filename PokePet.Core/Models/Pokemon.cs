using PokePet.Core.Enums;
using SQLite;
using System.Text.Json.Serialization;

namespace PokePet.Core.Models
{
	[Table("pokemon")]
	public class Pokemon
	{
		[PrimaryKey, AutoIncrement]
		public int PokemonId { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }
		
		private string? _name;
		[JsonPropertyName("name")]
		public string? Name
		{
			get => _name;
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
					_name = textInfo.ToTitleCase(value.ToLower());
				}
				else
				{
					_name = value;
				}
			}
		}

		public Hunger Hunger { get; set; }
		public Tiredness Tiredness { get; set; }
		public Boredom Boredom { get; set; }
	}

	public class PokemonResponse
	{
		[JsonPropertyName("babies")]
		public List<Pokemon>? Babies { get; set; }
	}
}
