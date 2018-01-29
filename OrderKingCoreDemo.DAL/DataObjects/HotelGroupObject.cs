using System.Collections.Generic;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class HotelGroupObject : BaseDataObject
    {
	    public string Name { get; set; }
	    public string Description { get; set; }
	    public string Phone { get; set; }
	    public string LogoUrl { get; set; }
	    public string ImageUrl { get; set; }
	    public List<HotelObject> Hotels { get; set; }
	}
}
