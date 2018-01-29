using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices
{
    public interface IServicesDataService
    {
		Task<RequestResult<List<ServiceObject>>> GetAllServices(string hotelId, CancellationToken cts);
		Task<RequestResult<ServiceObject>> GetService(string serviceId, CancellationToken cts);
		Task<RequestResult> ServiceOrder(string serviceId, DateTime dateTime, string token, CancellationToken cts);
	    Task<RequestResult<ServiceOrderObject>> GetOrder(string orderId, string token, CancellationToken cts);
	}
}