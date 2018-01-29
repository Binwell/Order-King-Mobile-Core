using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Restaurants
{
    public class MealOrderViewModel: BaseViewModel {
		public ICommand GoToCartCommand => MakeNavigateToCommand(Pages.Cart, NavigationMode.Modal, newNavigationStack: true);
	}
}
