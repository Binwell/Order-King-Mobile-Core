using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class FitnessDataService : BaseMockDataService, IFitnessDataService {
		public Task<RequestResult<List<ServiceObject>>> GetAllFitness(string hotelId, CancellationToken cts) {
			return GetMockDataList<ServiceObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Fitness.FitnessList.json");
		}

		public async Task<RequestResult<ServiceObject>> GetFitness(string spaId, CancellationToken cts) {
			var data = await GetMockDataList<ServiceObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Fitness.FitnessList.json");
			var fitness = data.Data.Where(x => x.Id == spaId);
			return new RequestResult<ServiceObject>(fitness.FirstOrDefault(), data.Status, data.Message);
		}

		public Task<RequestResult> FitnessOrder(string serviceId, DateTime dateTime, string token, CancellationToken cts) {
			return new Task<RequestResult>(() => new RequestResult(RequestStatus.Ok));
		}

		public Task<RequestResult<ServiceOrderObject>> GetOrder(string orderId, string token, CancellationToken cts)
		{
			return new Task<RequestResult<ServiceOrderObject>>(() => new RequestResult<ServiceOrderObject>(null,RequestStatus.Ok));
		}
	}
}