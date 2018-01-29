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

			NavigationService.Init(this);
			DialogService.Init(this);

			NavigationService.Instance.SetMainMasterDetailPage(Pages.Menu, Pages.HotelInfo);
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
