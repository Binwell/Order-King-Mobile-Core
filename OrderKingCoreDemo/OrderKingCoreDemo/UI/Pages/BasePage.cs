using System;
using System.Threading.Tasks;
using OrderKingCoreDemo.BL.ViewModels;
using Xamarin.Forms;

namespace OrderKingCoreDemo.UI.Pages
{
	public class BasePage : ContentPage, IDisposable {
		protected BaseViewModel BaseViewModel { get; private set; }

		~BasePage() {
			Dispose();
		}

		public void Dispose() {
			BaseViewModel?.Dispose();
		}

		public void SetViewModel(BaseViewModel viewModel) {
			BindingContext = viewModel;
		}

		protected override void OnAppearing() {
			base.OnAppearing();
			Task.Run(async () => {
				await Task.Delay(50); // Allow UI to handle events loop
				if (BaseViewModel != null)
					await BaseViewModel.OnPageAppearing();
			});
		}

		protected override void OnDisappearing() {
			base.OnDisappearing();
			Task.Run(async () => {
				await Task.Delay(50); // Allow UI to handle events loop
				if (BaseViewModel != null)
					await BaseViewModel.OnPageDissapearing();
			});
		}

	}

    public class BasePage<T>: BasePage where T: BaseViewModel {
	    public T ViewModel => BaseViewModel as T;
    }
}
