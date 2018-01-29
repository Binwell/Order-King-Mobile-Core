namespace OrderKingCoreDemo.DAL.DataObjects {
	public class MealOrderObject: MealObject {
		public int Count { get; set; }
		public MealOptionObject MealOption { get; set; }
	}
}