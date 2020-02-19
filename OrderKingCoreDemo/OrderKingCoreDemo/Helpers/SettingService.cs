using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OrderKingCoreDemo.Helpers
{
	public static class SettingService
	{
		static readonly object _locker = new object();
		static Func<IDictionary<string, object>> _settingsFunc;
		static Func<Task> _saveFunc;
		static IDictionary<string, object> Settings => _settingsFunc?.Invoke();

		public static void Init(Application app)
		{
			_settingsFunc = ()=> app.Properties;
			_saveFunc = app.SavePropertiesAsync;
		}

		public static string HotelId
		{
			get => Get<string>();
			set => Set(value);
		}

		#region Internal

		static T Get<T>([CallerMemberNameAttribute] string key = null)
		{
			lock (_locker)
			{
				var settings = Settings;
				if (settings.TryGetValue(key, out var value))

					if (value is T typedValue) return typedValue;

				return default;
			}

		}

		static void Set<T>(T value,[CallerMemberNameAttribute] string key = null)
		{
			lock (_locker)
			{
				var settings = Settings;
				if (settings.ContainsKey(key))
					Settings[key] = value;
				else
					settings.Add(key, value);
			}
			Task.Run(()=>_saveFunc?.Invoke());
		}

		#endregion



	}
}
