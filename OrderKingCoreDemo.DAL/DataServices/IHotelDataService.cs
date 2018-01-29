using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices
{
    public interface IHotelDataService {
		Task<RequestResult<HotelGroupObject>> GetHotelGroupInfo(CancellationToken cts);
		Task<RequestResult<HotelObject>> GetHotelInfo(string hotelId, CancellationToken cts);
    }
}