using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class BasketObject
    {
        public List<MealObject> MealList { get; set; }
		public double Summ { get; set; }
		public int PersonQuantity { get; set; }
		public List<string> CheckMethod { get; set; }
		public List<string> PayMethod { get; set; }
    }
}