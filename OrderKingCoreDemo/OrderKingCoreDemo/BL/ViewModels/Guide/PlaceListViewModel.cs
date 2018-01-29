using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Guide
{
    public class PlaceListViewModel: BaseViewModel {
	    public ICommand GoToPlaceInfoCommand => MakeNavigateToCommand(Pages.PlaceInfo);
    }
}
