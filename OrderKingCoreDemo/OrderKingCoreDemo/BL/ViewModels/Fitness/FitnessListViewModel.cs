using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Fitness
{
    public class FitnessListViewModel: BaseViewModel {
	    public ICommand GoToFitnessOrderCommand => MakeNavigateToCommand(Pages.FitnessOrder);
    }
}
