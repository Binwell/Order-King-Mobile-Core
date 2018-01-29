using System;

namespace OrderKingCoreDemo.DAL.DataObjects {
	public class UserObject : BaseDataObject {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Phone { get; set; }
	    public string Email { get; set; }
		public Gender Gender { get; set; } 
		public DateTime Birthday { get; set; }
		public string FullName => $"{FirstName} {LastName}";
	}
}