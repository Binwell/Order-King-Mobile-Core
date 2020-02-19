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
		public App ()
		{
			InitializeComponent();
			SettingService.Init(this);
			DialogService.Init(this);
			DataServices.Init(true);
			NavigationService.Init(Pages.HotelInfo);
 		}

		protected override void OnStart ()
		{
			// Handle when your app starts
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
