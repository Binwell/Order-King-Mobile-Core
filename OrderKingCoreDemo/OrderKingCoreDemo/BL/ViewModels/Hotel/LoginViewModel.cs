using System.Windows.Input;
using OrderKingCoreDemo.Helpers;

namespace OrderKingCoreDemo.BL.ViewModels.Hotel
{
    public class LoginViewModel: BaseViewModel
    {
	    public ICommand LoginCommand => MakeCommand(LoginCommandImplementation);

	    void LoginCommandImplementation()
	    {
		    SettingService.HotelId = "exampleHotelId";
		    NavigateTo(Pages.HotelInfo, mode:NavigationMode.Master);
	    }
    }
}
