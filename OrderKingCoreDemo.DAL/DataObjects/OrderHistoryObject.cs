using System;
using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class OrderHistoryObject : BaseDataObject {
		public string OrderNumber { get; set; }
		public double OrderPrice { get; set; }
		public string Status { get; set; }
		public DateTime Date { get; set; }
		public PaymentType PaymentType { get; set; }
		public ConfirmType ConfirmType { get; set; }
		public List<MealOrderObject> MealsInOrder { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public OrderType OrderType { get; set; }
	}
}