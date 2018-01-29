using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class ServicesDataService : BaseMockDataService, IServicesDataService {
		public Task<RequestResult<List<ServiceObject>>> GetAllServices(string hotelId, CancellationToken cts) {
			return GetMockDataList<ServiceObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Services.ServiceList.json");
		}

		public async Task<RequestResult<ServiceObject>> GetService(string serviceId, CancellationToken cts) {
			var data = await GetMockDataList<ServiceObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Services.ServiceList.json");
			var service = data.Data.Where(x => x.Id == serviceId);
			return new RequestResult<ServiceObject>(service.FirstOrDefault(),data.Status,data.Message );
		}

		public Task<RequestResult> ServiceOrder(string serviceId, DateTime dateTime, string token, CancellationToken cts) {
			return new Task<RequestResult>(()=>new RequestResult(RequestStatus.Ok));
		}

		public Task<RequestResult<ServiceOrderObject>> GetOrder(string orderId, string token, CancellationToken cts)
		{
			return new Task<RequestResult<ServiceOrderObject>>(() => new RequestResult<ServiceOrderObject>(null,RequestStatus.Ok));
		}
	}
}