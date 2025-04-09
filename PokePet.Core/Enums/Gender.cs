using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokePet.Core.Enums
{
	public enum Gender
	{
		[Description("Male")]
		Male,

		[Description("Female")]
		Female,

		[Description("Non Binary")]
		NonBinary
	}
}
