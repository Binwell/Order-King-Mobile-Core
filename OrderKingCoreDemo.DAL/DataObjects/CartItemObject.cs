namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class CartItemObject
    {
	    public string MealId { get; set; }
	    public string MealItemId { get; set; }
	    public string Name { get; set; }
	    public string OptionName { get; set; }
	    public string ImageUrl { get; set; }
	    public int Count { get; set; }
	    public double Price { get; set; }
	    public double Discount { get; set; }
    }
}
