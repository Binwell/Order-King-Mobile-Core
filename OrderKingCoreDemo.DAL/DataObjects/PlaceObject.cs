namespace OrderKingCoreDemo.DAL.DataObjects {
	public class PlaceObject:BaseDataObject {
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string LogoUrl { get; set; }
		public string Address { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public string Description { get; set; }
		public string Phone { get; set; }
		public string WorkTime { get; set; }
	}
}