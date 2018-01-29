using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices {
	public interface IGuideDataService {
		Task<RequestResult<List<GuideCategoryObject>>> GetAllGuideCatigories(string hotelId,CancellationToken cts);
		Task<RequestResult<List<PlaceObject>>> GetAllPlacesForGuideCatigory(string guideCategoryId, string hotelId, CancellationToken cts);
		Task<RequestResult<PlaceObject>> GetPlace(string placeId, CancellationToken cts);
	}
}