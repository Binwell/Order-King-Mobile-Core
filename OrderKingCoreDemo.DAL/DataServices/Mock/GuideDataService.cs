using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class GuideDataService : BaseMockDataService, IGuideDataService {
		public Task<RequestResult<List<GuideCategoryObject>>> GetAllGuideCatigories(string hotelId, CancellationToken cts) {
			return GetMockDataList<GuideCategoryObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Guide.GuideCategories.json");
		}

		public Task<RequestResult<List<PlaceObject>>> GetAllPlacesForGuideCatigory(string guideCategoryId, string hotelId, CancellationToken cts) {
			return GetMockDataList<PlaceObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Guide.GuidePlaces.json");
		}

		public async Task<RequestResult<PlaceObject>> GetPlace(string placeId, CancellationToken cts) {
			var data = await GetMockDataList<PlaceObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Guide.GuidePlaces.json");
			var place = data.Data.Where(x => x.Id == placeId);
			return new RequestResult<PlaceObject>(place.FirstOrDefault(), data.Status, data.Message);
		}
	}
}