using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Restaurants
{
    public class RestaurantInfoViewModel: BaseViewModel {
	    public ICommand GoToCartCommand => MakeNavigateToCommand(Pages.Cart, NavigationMode.Modal, newNavigationStack: true);
	    public ICommand GoToRestaurantMealListCommand => MakeNavigateToCommand(Pages.RestaurantMealList);
    }
}
