using System.ComponentModel;

namespace PokePet.Core.Enums
{
	public enum Hunger
	{
		[Description("Full")]
		Full,

		[Description("Peckish")]
		Peckish,

		[Description("Hungry")]
		Hungry,

		[Description("Starving")]
		Starving
	}
}
