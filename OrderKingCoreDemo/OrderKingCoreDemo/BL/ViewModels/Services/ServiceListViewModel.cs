using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Services
{
    public class ServiceListViewModel: BaseViewModel {
	    public ICommand GoToServiceOrderCommand => MakeNavigateToCommand(Pages.ServiceOrder);
    }
}
