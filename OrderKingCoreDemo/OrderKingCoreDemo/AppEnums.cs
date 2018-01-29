namespace OrderKingCoreDemo
{
	public enum Pages {
		HotelInfo,
		Menu,
		Login,
		ServiceList,
		ServiceOrder,
		RestaurantList,
		RestaurantInfo,
		RestaurantMealList,
		MealOrder,
		Cart,
		FitnessList,
		FitnessOrder,
		GuideCategories,
		PlaceList,
		PlaceInfo,
		ChatCategories,
		Chat,
		Profile,
		ProfileEdit,
		NotificationList,
		NotificationInfo,
		OrderHistory,
		OrderInfo
	}


	public enum NavigationMode {
		Normal,
		Modal,
		RootPage,
		Custom
	}

	public enum PageState {
		Clean,
		Loading,
		Normal,
		NoData,
		Error,
		NoInternet
	}
}
