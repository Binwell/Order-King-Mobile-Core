using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Profile
{
    public class NotificationListViewModel: BaseViewModel {
	    public ICommand GoToNotificationInfoCommand => MakeNavigateToCommand(Pages.NotificationInfo);
    }
}
