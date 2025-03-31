using PokePet.Core.Enums;
using PokePet.Core.Features;
using SQLite;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PokePet.Core.Models
{
	[Table("pokemon")]
	public class Pokemon : INotifyPropertyChanged
	{
		private readonly IDispatcherTimer _timer;

		public Pokemon()
		{
			Happiness = 3;

			_timer = Application.Current?.Dispatcher?.CreateTimer()	?? throw new InvalidOperationException("Timer could not be created.");
			_timer.Interval = TimeSpan.FromSeconds(1);
			_timer.Tick += (s, e) =>
			{
				OnPropertyChanged(nameof(TimeUntilNextFeed));
				OnPropertyChanged(nameof(TimeUntilNextPlay));
				OnPropertyChanged(nameof(TimeUntilNextSleep));
				OnPropertyChanged(nameof(CanFeed));
				OnPropertyChanged(nameof(CanPlay));
				OnPropertyChanged(nameof(CanSleep));
			};
			_timer.Start();
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
				OnPropertyChanged(nameof(Hunger));
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
				OnPropertyChanged(nameof(Tiredness));
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
				OnPropertyChanged(nameof(Boredom));
			}
		}

		private int _happiness;
		public int Happiness
		{
			get => _happiness;
			set {
				_happiness = value;
				OnPropertyChanged(nameof(Happiness));
			}
		}

		protected void OnPropertyChanged(string propertyName) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		public string ImagePath => $"{Regex.Replace(Name ?? string.Empty, "-", "").ToLower()}.png";

		public DateTime LastSaved { get; set; } = DateTime.MinValue;
		public DateTime LastFed { get; set; } = DateTime.MinValue;
		public DateTime LastPlayed { get; set; } = DateTime.MinValue;
		public DateTime LastSlept { get; set; } = DateTime.MinValue;
		public bool CanFeed() => (DateTime.UtcNow - LastFed).TotalMinutes >= 60 && Hunger > Hunger.Full;
		public bool CanPlay() => (DateTime.UtcNow - LastPlayed).TotalMinutes >= 60 && Boredom > Boredom.NotBored;
		public bool CanSleep() => (DateTime.UtcNow - LastSlept).TotalMinutes >= 60 && Tiredness > Tiredness.Awake;
		public string TimeUntilNextFeed => GetTimeRemaining(LastFed);
		public string TimeUntilNextPlay => GetTimeRemaining(LastPlayed);
		public string TimeUntilNextSleep => GetTimeRemaining(LastSlept);

		public event PropertyChangedEventHandler? PropertyChanged;

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
				Hunger = (Hunger)Math.Min((int)Hunger + minutesElapsed / 60, (int)Hunger.Starving);
				Tiredness = (Tiredness)Math.Min((int)Tiredness + minutesElapsed / 60, (int)Tiredness.Exhausted);
				Boredom = (Boredom)Math.Min((int)Boredom + minutesElapsed / 60, (int)Boredom.ReallyBored);
			}

			// Update LastSaved to the current time after applying changes
			LastSaved = DateTime.Now;
		}

		public void SaveState()
		{
			LastSaved = DateTime.UtcNow;
		}

		public void Feed()
		{
			if (CanFeed())
			{
				Hunger = (Hunger)Math.Max((int)Hunger - 1, (int)Hunger.Full);
				LastFed = DateTime.UtcNow;
			}
		}

		public void Sleep()
		{
			if (CanSleep())
			{
				Tiredness = (Tiredness)Math.Max((int)Tiredness - 1, (int)Tiredness.Awake);
				LastSlept = DateTime.UtcNow;
			}
		}

		public void Play()
		{
			if (CanPlay())
			{
				Boredom = (Boredom)Math.Max((int)Boredom - 1, (int)Boredom.NotBored);
				LastPlayed = DateTime.UtcNow;
			}
		}

		private string GetTimeRemaining(DateTime lastAction)
		{
			double minutesLeft = 60 - (DateTime.UtcNow - lastAction).TotalMinutes;
			double secondsLeft = 60 - (DateTime.UtcNow - lastAction).Seconds;
			return minutesLeft > 0 ? $"{(int)minutesLeft} min {secondsLeft} sec" : "Available!";
		}

		#endregion
	}


	public class PokemonResponse
	{
		[JsonPropertyName("babies")]
		public List<Pokemon>? Babies { get; set; }
	}
}
