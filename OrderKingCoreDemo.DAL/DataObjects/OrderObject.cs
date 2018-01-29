using System;
using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class OrderObject : BaseDataObject {
		public DateTime Date { get; set; }
		public int OrderNumber { get; set; }
		public string Status { get; set; }
		public string PayMethod { get; set; }
		public List<MealObject> Meals { get; set; }
	}
}