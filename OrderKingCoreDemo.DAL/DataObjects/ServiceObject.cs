namespace OrderKingCoreDemo.DAL.DataObjects {
	public class ServiceObject : BaseDataObject {
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public bool Feature { get; set; }
	}
}