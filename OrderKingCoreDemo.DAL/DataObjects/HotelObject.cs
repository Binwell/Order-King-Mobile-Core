using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class HotelObject : BaseDataObject {
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public PositionObject Coordinates { get; set; }
		public string ImageUrl { get; set; }
		public List<PayMethod> PayMethodList { get; set; }
		public List<CheckMethod> CheckMethodList { get; set; }
	}

	public class CheckMethod : BaseDataObject {
		public string Name { get; set; }
	}

	public class PayMethod : BaseDataObject {
		public string Name { get; set; }
	}
}