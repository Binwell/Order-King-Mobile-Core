using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class RestaurantsObject : BaseDataObject {
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public bool Feature { get; set; }
		public List<MenuCategory> MenuCategoryList { get; set; }
	}
	public class MenuCategory {
		public string Id { get; set; }
		public string Name { get; set; }
	}
}