using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices {
	public interface IRestaurantDataService
	{
	    Task<RequestResult<List<RestaurantsObject>>> GetAllRestaurants(string hotelId, CancellationToken cts);
		Task<RequestResult<RestaurantsObject>> GetRestaurant(string restaurantId, CancellationToken cts);
		Task<RequestResult<List<MealObject>>> GetAllMeals(string menuId, CancellationToken cts);
		Task<RequestResult<MealObject>> GetMeal(string mealId, CancellationToken cts);
		Task<RequestResult<ServiceOrderObject>> GetOrder(string orderId, string token, CancellationToken cts);
		Task<RequestResult> MakeOrder(CartObject basket, string token, CancellationToken cts);
	}
}