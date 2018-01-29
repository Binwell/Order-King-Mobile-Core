using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Hotel
{
    public class MenuViewModel: BaseViewModel {
	    public ICommand GoToProfileCommand => MakeMenuCommand(Pages.Profile);
	    public ICommand GoToHotelInfoCommand => MakeMenuCommand(Pages.HotelInfo);
	    public ICommand GoToLoginCommand => MakeMenuCommand(Pages.Login);
	    public ICommand GoToServiceListCommand => MakeMenuCommand(Pages.ServiceList);
	    public ICommand GoToFitnessListCommand => MakeMenuCommand(Pages.FitnessList);
	    public ICommand GoToGuideCategoriesCommand => MakeMenuCommand(Pages.GuideCategories);
	    public ICommand GoToChatCategoriesCommand => MakeMenuCommand(Pages.ChatCategories);
	    public ICommand GoToRestaurantListCommand => MakeMenuCommand(Pages.RestaurantList);

		static ICommand MakeMenuCommand(object page) {
			return MakeNavigateToCommand(page, NavigationMode.RootPage, newNavigationStack: true, withAnimation: false);
		}
    }
}
