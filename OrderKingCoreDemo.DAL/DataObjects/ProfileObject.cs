using System;

namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class ProfileObject
    {
        public string HotelId { get; set; }
		public DateTime? DateIn { get; set; }
		public DateTime? DateOut { get; set; }
		public string RoomNumber { get; set; }
		public UserObject User { get; set; }
	}
}