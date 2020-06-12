using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace OrderKingCoreDemo.UI.Converters
{
	public static class ConverterFactory
	{
		static readonly Dictionary<object, IValueConverter> _cachedConverters = new Dictionary<object, IValueConverter>();

		public static IValueConverter Make(Func<object, object> func)
		{
			if (_cachedConverters.TryGetValue(func, out var converter)) return converter;
			converter = new Converter(func);
			_cachedConverters.Add(func, converter);
			return converter;
		}

		public static IValueConverter Make(Func<object, object> func, object defaultValue)
		{
			if (_cachedConverters.TryGetValue(func, out var converter)) return converter;
			converter = new Converter(func, defaultValue);
			_cachedConverters.Add(func, converter);
			return converter;
		}

		public static IValueConverter Make<T>(Func<T, object> func)
		{
			if (_cachedConverters.TryGetValue(func, out var converter)) return converter;
			converter = new Converter<T>(func);
			_cachedConverters.Add(func, converter);
			return converter;
		}

		public static IValueConverter Make<T>(Func<T, object> func, object defaultValue)
		{
			if (_cachedConverters.TryGetValue(func, out var converter)) return converter;
			converter = new Converter<T>(func, defaultValue);
			_cachedConverters.Add(func, converter);
			return converter;
		}

		public static IValueConverter FromDictionary(IDictionary dictionary)
		{
			if (_cachedConverters.TryGetValue(dictionary, out var converter)) return converter;
			converter = new DictionaryConverter(dictionary);
			_cachedConverters.Add(dictionary, converter);
			return converter;
		}

		public static IValueConverter FromDictionary(IDictionary dictionary, object defaultValue)
		{
			if (_cachedConverters.TryGetValue(dictionary, out var converter)) return converter;
			converter = new DictionaryConverter(dictionary, defaultValue);
			_cachedConverters.Add(dictionary, converter);
			return converter;
		}

		class Converter : IValueConverter
		{
			readonly object _defaultValue;
			readonly Func<object, object> _func;

			public Converter(Func<object, object> func, object defaultValue = null)
			{
				_func = func;
				_defaultValue = defaultValue;
			}

			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				try
				{
					return _func(value);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return _defaultValue ?? GetDefaultValue(targetType);
				}
			}

			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new NotImplementedException();
			}
		}


		class DictionaryConverter : Converter
		{
			public DictionaryConverter(IDictionary dictionary, object defaultValue = null) : base(
				value => value == null ? defaultValue : dictionary[value], defaultValue)
			{
			}
		}

		class Converter<T> : Converter
		{
			public Converter(Func<T, object> func, object defaultValue = null) : base(o => func.Invoke((T) o), defaultValue)
			{
			}
		}

		static object GetDefaultValue(Type t)
		{
			var underlying = Nullable.GetUnderlyingType(t);
			if (underlying != null)
				return Activator.CreateInstance(underlying);
			return Activator.CreateInstance(t);
		}
	}
}