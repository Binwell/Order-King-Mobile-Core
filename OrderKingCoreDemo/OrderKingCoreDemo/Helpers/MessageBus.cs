using System;
using Xamarin.Forms;

namespace OrderKingCoreDemo.Helpers {
	public class MessageBus {
		static readonly Lazy<MessageBus> LazyInstance = new Lazy<MessageBus>(() => new MessageBus(), true);
		static MessageBus Instance => LazyInstance.Value;

		MessageBus() {
		}

		public static void SendMessage(string message) {
			MessagingCenter.Send(Instance, message);
		}

		public static void SendMessage<TArgs>(string message, TArgs args) {
			MessagingCenter.Send(Instance, message, args);
		}
	}
}