using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using OrderKingCoreDemo.UI.Converters.Implementations;
using Xamarin.Forms;

namespace OrderKingCoreDemo.UI.Converters
{
	public static class StaticConverters
	{

		public static IValueConverter BoolInvertConverter => ConverterFactory.Make<bool>(e => !e);
		public static IValueConverter NullToInvisibleConverter => ConverterFactory.Make<object>(e => e != null);

		public static IValueConverter StringHexToColorConverter => MakeConverter(typeof(StringHexToColorConverter));


		#region Internal

		static readonly ConcurrentDictionary<string, IValueConverter>
			_cachedConverters = new ConcurrentDictionary<string, IValueConverter>();
		static IValueConverter MakeConverter(Type converterType, [CallerMemberName] string propertyName = null)
		{
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			if (!_cachedConverters.ContainsKey(propertyName))
			{
				var converter = Activator.CreateInstance(converterType) as IValueConverter;
				if (converter == null) return null;
				_cachedConverters.TryAdd(propertyName, converter);
				return converter;
			}

			if (_cachedConverters.TryGetValue(propertyName, out var cachedCommand))
				return cachedCommand;

			throw new ArgumentOutOfRangeException(nameof(propertyName));
		}

		#endregion
	}



}