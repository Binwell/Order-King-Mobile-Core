using System;
using System.Globalization;
using Xamarin.Forms;

namespace OrderKingCoreDemo.UI.Converters.Implementations
{

	public class StringHexToColorConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string str)
			{
				var color = Color.FromHex(str);
				return color;
			}

			return Color.Default;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}