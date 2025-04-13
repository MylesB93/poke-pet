using PokePet.Core.Enums;

namespace PokePet.Core.Features
{
	public class HappinessCalculator
	{
		public static int CalculateHappiness(Boredom boredom, Hunger hunger, Tiredness tiredness)
		{
			// Calculate the average
			int total = (int)boredom + (int)hunger + (int)tiredness;
			int average = total / 3;

			// Invert the value to represent happiness
			int happiness = 3 - average;

			// Make sure the result is within the range of 0 to 3
			return Math.Clamp(happiness, 0, 3);
		}
	}
}
