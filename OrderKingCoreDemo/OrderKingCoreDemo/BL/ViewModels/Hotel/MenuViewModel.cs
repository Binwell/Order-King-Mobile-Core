using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using OrderKingCoreDemo.DAL.DataObjects;
using OrderKingCoreDemo.DAL.DataServices;
using OrderKingCoreDemo.Helpers;

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

	    public List<RestaurantsObject> RestaurantItemsSource
	    {
		    get => Get<List<RestaurantsObject>>();
		    set => Set(value);
	    }
		static ICommand MakeMenuCommand(object page) {
			return MakeNavigateToCommand(page, NavigationMode.RootPage, newNavigationStack: true, withAnimation: false);
		}

		protected override async Task LoadDataAsync()
		{
			var response = await DataServices.Restaurant.GetAllRestaurants(SettingService.HotelId, CancellationToken);
			if (response.IsValid)
				RestaurantItemsSource = response.Data;
		}
    }
}
