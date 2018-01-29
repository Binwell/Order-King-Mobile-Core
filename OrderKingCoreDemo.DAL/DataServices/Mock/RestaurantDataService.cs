using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class RestaurantDataService : BaseMockDataService, IRestaurantDataService {
		public Task<RequestResult<List<RestaurantsObject>>> GetAllRestaurants(string hotelId, CancellationToken cts) {
			return GetMockDataList<RestaurantsObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Restaurants.RestaurantList.json");
		}

		public Task<RequestResult<RestaurantsObject>> GetRestaurant(string restaurantId, CancellationToken cts) {
			return GetMockData<RestaurantsObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Restaurants.RestaurantInfo.json");
		}

		public Task<RequestResult<List<MealObject>>> GetAllMeals(string menuId, CancellationToken cts) {
			return GetMockDataList<MealObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Restaurants.Meals.json");
		}

		public Task<RequestResult<MealObject>> GetMeal(string mealId, CancellationToken cts) {
			return GetMockData<MealObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Restaurants.Meal.json");
		}

		public Task<RequestResult<ServiceOrderObject>> GetOrder(string orderId, string token, CancellationToken cts)
		{
			return new Task<RequestResult<ServiceOrderObject>>(() => new RequestResult<ServiceOrderObject>(null,RequestStatus.Ok));
		}

		public Task<RequestResult> MakeOrder(CartObject basket, string token, CancellationToken cts)
		{
			return new Task<RequestResult>(() => new RequestResult(RequestStatus.Ok));
		}
		
	}
}