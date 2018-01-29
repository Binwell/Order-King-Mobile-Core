using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Hotel
{
    public class LoginViewModel: BaseViewModel {
	    public ICommand GoToHotelInfoCommand => MakeNavigateToCommand(Pages.HotelInfo, NavigationMode.RootPage, newNavigationStack: true);
    }
}
