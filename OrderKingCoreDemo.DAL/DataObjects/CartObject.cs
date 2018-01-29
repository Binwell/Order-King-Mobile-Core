using System;
using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class CartObject
    {
        public List<CartItemObject> MealList { get; set; }
		public double OrderPrice { get; set; }
	    public int PersonQuantity { get; set; }
		public ConfirmType CheckMethod { get; set; }
		public PaymentType PayMethod { get; set; }
		public DateTime Date { get; set; }
    }
}