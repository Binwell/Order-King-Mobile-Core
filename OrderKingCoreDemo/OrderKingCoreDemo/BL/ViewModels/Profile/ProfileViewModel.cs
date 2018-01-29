using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Profile
{
    public class ProfileViewModel: BaseViewModel {
	    public ICommand GoToNotificationListCommand => MakeNavigateToCommand(Pages.NotificationList);
	    public ICommand GoToProfileEditCommand => MakeNavigateToCommand(Pages.ProfileEdit);
	    public ICommand GoToOrderHistoryCommand => MakeNavigateToCommand(Pages.OrderHistory);
	}
}
