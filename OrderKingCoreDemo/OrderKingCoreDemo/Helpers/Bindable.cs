/* MIT License

Copyright (c) 2018 Binwell https://binwell.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderKingCoreDemo.Helpers {
	public class Bindable : INotifyPropertyChanged, INotifyPropertyChanging {
		readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangingEventHandler PropertyChanging;

		protected bool CallPropertyChangeEvent { get; set; } = true;

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanging([CallerMemberName] string propertyName = null) {
			PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
		}

		protected T Get<T>(T defValue = default(T), [CallerMemberName] string name = null) {
			if (string.IsNullOrEmpty(name)) return defValue;
			if (_properties.TryGetValue(name, out var value))
				return (T) value;
			_properties.AddOrUpdate(name, defValue, (s, o) => defValue);
			return defValue;
		}

		protected bool Set(object value, [CallerMemberName] string name = null) {
			if (string.IsNullOrEmpty(name))
				return false;

			var isExists = _properties.TryGetValue(name, out var getValue);
			if (isExists && Equals(value, getValue))
				return false;

			if (CallPropertyChangeEvent)
				OnPropertyChanging(name);

			_properties.AddOrUpdate(name, value, (s, o) => value);

			if (CallPropertyChangeEvent)
				OnPropertyChanged(name);

			return true;
		}
	}
}