using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class HotelDataService : BaseMockDataService, IHotelDataService {
		public Task<RequestResult<HotelGroupObject>> GetHotelGroupInfo(CancellationToken cts)
		{
			return GetMockData<HotelGroupObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Hotel.HotelGroupInfo.json");
		}

		public Task<RequestResult<HotelObject>> GetHotelInfo(string hotelId, CancellationToken cts) {
			return GetMockData<HotelObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Hotel.HotelInfo.json");
		}
	}
}