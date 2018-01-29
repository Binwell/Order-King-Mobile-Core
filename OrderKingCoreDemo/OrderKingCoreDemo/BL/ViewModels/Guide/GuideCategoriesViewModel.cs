using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Guide
{
    public class GuideCategoriesViewModel: BaseViewModel {
	    public ICommand GoToPlaceListCommand => MakeNavigateToCommand(Pages.PlaceList);
    }
}
