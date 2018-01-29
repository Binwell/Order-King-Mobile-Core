using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Restaurants
{
    public class RestaurantMealListViewModel: BaseViewModel {
	    public ICommand GoToMealOrderCommand => MakeNavigateToCommand(Pages.MealOrder);
    }
}
