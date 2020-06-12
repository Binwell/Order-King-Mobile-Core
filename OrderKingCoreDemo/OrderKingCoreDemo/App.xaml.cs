using OrderKingCoreDemo.DAL.DataServices;
using OrderKingCoreDemo.Helpers;
using OrderKingCoreDemo.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OrderKingCoreDemo
{
	public partial class App : Application
	{
		/// <summary>
		/// DO NOT TOUCH APP()
		/// </summary>
		public App()
		{
			//Fix ios crash
			Current.MainPage = new ContentPage();
		}

		protected override async void OnStart ()
		{
			InitializeComponent();
			SettingService.Init(this);
			DialogService.Init(this);
			DataServices.Init(true);
			await NavigationService.Init(Pages.Login);
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
