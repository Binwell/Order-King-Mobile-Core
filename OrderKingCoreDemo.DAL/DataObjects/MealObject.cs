using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class MealObject : BaseDataObject {
		public string Name { get; set; }
		public string Recipe { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
		public double Discount { get; set; }
		public double OldPrice { get; set; }
		public double Price { get; set; }
		public List<MealOptionObject> MealOptions { get; set; }
	}
}
