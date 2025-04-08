using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace PokePet.Core.Enums
{
	public class EnumDescriptionConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is Enum enumVal)
			{
				var field = enumVal.GetType().GetField(enumVal.ToString());
				var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
				return attribute?.Description ?? enumVal.ToString();
			}

			return value?.ToString() ?? string.Empty;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}
