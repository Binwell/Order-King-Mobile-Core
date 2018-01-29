namespace OrderKingCoreDemo.DAL.DataObjects {
	public class MealOptionObject : BaseDataObject {
		public string Name { get; set; }
		public double Discount { get; set; }
	    public double Price { get; set; }
		public double OldPrice { get; set; }
	}
}