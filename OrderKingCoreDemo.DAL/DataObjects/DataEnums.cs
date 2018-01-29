namespace OrderKingCoreDemo.DAL.DataObjects
{
	public enum OrderStatus
	{
		Seen,
		Cancel,
		Next,
		Check,
		Made
	}
	
	public enum PaymentType
	{
		Cash = 0,
		RoomAccount = 1,
		CreditCard = 2
	}

	public enum ConfirmType
	{
		PhoneCall = 0
	}

	public enum OrderType
	{
		Service = 0,
		Restaurant = 1,
		Fitness = 2
	}

	public enum Gender
	{
		Male = 0,
		Female = 1
	}

	public enum OrderHistoryType
	{
		Restaurant,
		Service
	}

	public enum MessageStatus
	{
		Read,
		Received,
		Sent
	}
}