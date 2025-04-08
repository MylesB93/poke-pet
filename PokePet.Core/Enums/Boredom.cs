using System.ComponentModel;

namespace PokePet.Core.Enums
{
	public enum Boredom
	{
		[Description("Not Bored")]
		NotBored,

		[Description("Slightly Bored")]
		SlightlyBored,

		[Description("Bored")]
		Bored,

		[Description("Really Bored")]
		ReallyBored
	}
}
