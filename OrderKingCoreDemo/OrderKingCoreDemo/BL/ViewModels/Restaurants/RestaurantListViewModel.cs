using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Restaurants
{
    public class RestaurantListViewModel: BaseViewModel {
	    public ICommand GoToRestaurantInfoCommand => MakeNavigateToCommand(Pages.RestaurantInfo);
    }
}
