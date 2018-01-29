using System;

namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class ServiceOrderObject
    {
	    public string Id { get; set; }
	    public string ServiceId { get; set; }

	    public string Name { get; set; }

	    public string Description { get; set; }

	    public double Price { get; set; }

	    public OrderStatus OrderStatus { get; set; }

	    public string ImageUrl { get; set; }
        
	    public DateTime CreatedOrderDate { get; set; }

	    public DateTime RequestServiceDate { get; set; }
    }
}