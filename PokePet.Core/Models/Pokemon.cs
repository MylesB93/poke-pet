﻿using PokePet.Core.Enums;
using PokePet.Core.Features;
using SQLite;
using System.Text.Json.Serialization;

namespace PokePet.Core.Models
{
	[Table("pokemon")]
	public class Pokemon
	{
		public Pokemon()
		{
			Happiness = 3;
		}

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

		private Hunger _hunger;
		public Hunger Hunger
		{
			get => _hunger;
			set
			{
				_hunger = value;
				CalculateHappiness();
			}
		}

		private Tiredness _tiredness;
		public Tiredness Tiredness
		{
			get => _tiredness;
			set
			{
				_tiredness = value;
				CalculateHappiness();
			}
		}

		private Boredom _boredom;
		public Boredom Boredom
		{
			get => _boredom;
			set
			{
				_boredom = value;
				CalculateHappiness();
			}
		}

		private int _happiness;
		public int Happiness //Is there a better way to do this?
		{
			get => _happiness;
			set => _happiness = value;
		}

		public string ImagePath => $"{Name?.ToLower()}.png";

		public DateTime LastSaved { get; set; } = DateTime.MinValue;

		private void CalculateHappiness()
		{
			Happiness = HappinessCalculator.CalculateHappiness(Boredom, Hunger, Tiredness);
		}

		#region methods
		public void LoadLastState()
		{
			if (LastSaved == DateTime.MinValue)
			{
				// If LastSaved has never been set, initialize it to now.
				LastSaved = DateTime.Now;
				return;
			}

			// Calculate minutes elapsed since the last save
			int minutesElapsed = (int)(DateTime.Now - LastSaved).TotalMinutes;

			if (minutesElapsed > 0) // Only update if some time has passed
			{
				Hunger = (Hunger)Math.Min((int)Hunger + minutesElapsed / 1, (int)Hunger.Starving);
				Tiredness = (Tiredness)Math.Min((int)Tiredness + minutesElapsed / 1, (int)Tiredness.Exhausted);
				Boredom = (Boredom)Math.Min((int)Boredom + minutesElapsed / 1, (int)Boredom.ReallyBored);
			}

			// Update LastSaved to the current time after applying changes
			LastSaved = DateTime.Now;
		}

		public void SaveState()
		{
			LastSaved = DateTime.UtcNow;
		}

		private void ApplyElapsedTime(TimeSpan elapsed)
		{
			int minutesElapsed = (int)elapsed.TotalMinutes;

			//Increase stats over time
			Hunger = (Hunger)Math.Min((int)Hunger + minutesElapsed / 1, (int)Hunger.Starving);
			Tiredness = (Tiredness)Math.Min((int)Tiredness + minutesElapsed / 1, (int)Tiredness.Exhausted);
			Boredom = (Boredom)Math.Min((int)Boredom + minutesElapsed / 1, (int)Boredom.ReallyBored);
		}

		#endregion
	}


	public class PokemonResponse
	{
		[JsonPropertyName("babies")]
		public List<Pokemon>? Babies { get; set; }
	}
}
