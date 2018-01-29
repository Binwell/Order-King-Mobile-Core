using System;

namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class NotificationObject:BaseDataObject
    {
        public string Name { get; set; }
		public string Description { get; set; }
        public string ImageUrl { get; set; }
		public DateTime Date { get; set; }
    }
}