using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class ProfileDataService : BaseMockDataService, IProfileDataService {
		public Task<RequestResult<ProfileObject>> Login(string password, string token, CancellationToken cts) {
			return GetMockData<ProfileObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Hotel.Profile.json");
		}

		public async Task<RequestResult<string>> GetPassword(string phone, CancellationToken cts) {
			return new RequestResult<string>(@"MockToken",RequestStatus.Ok);
		}

		public Task<RequestResult<ProfileObject>> Register(ProfileObject userInfo, string token, CancellationToken cts) {
			return GetMockData<ProfileObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Hotel.Profile.json");
		}
		public Task<RequestResult> CancelPlacement(string token, CancellationToken cts) {
			return new Task<RequestResult>(() => new RequestResult(RequestStatus.Ok));
		}

		public Task<RequestResult<ProfileObject>> ChangeUserInfo(ProfileObject newUserInfo, string token, CancellationToken cts)
		{
			return GetMockData<ProfileObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Hotel.Profile.json");
		}

		public Task<RequestResult<ProfileObject>> GetUserInfo(string token, CancellationToken cts)
		{
			return GetMockData<ProfileObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Hotel.Profile.json");
		}

		public Task<RequestResult<List<NotificationObject>>> GetAllNotification(string token, CancellationToken cts) {
			return GetMockDataList<NotificationObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Profile.NotificationList.json");
		}

		public async Task<RequestResult<NotificationObject>> GetNotification(string notifyId, string token, CancellationToken cts) {
			var data = await GetMockDataList<NotificationObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Profile.NotificationList.json");
			var notification = data.Data.Where(x => x.Id == notifyId);
			return new RequestResult<NotificationObject>(notification.FirstOrDefault(), data.Status, data.Message);
		}

		public Task<RequestResult<List<OrderHistoryObject>>> GetOrderHistoryList(string token, CancellationToken cts) {
			return GetMockDataList<OrderHistoryObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Profile.OrderHistoryList.json");
		}

		public async Task<RequestResult<OrderHistoryObject>> GetOrder(string orderId, OrderType type, string token, CancellationToken cts) {
			var data = await GetMockDataList<OrderHistoryObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Profile.OrderHistoryList.json");
			var order = data.Data.Where(x => x.Id == orderId);
			return new RequestResult<OrderHistoryObject>(order.FirstOrDefault(), data.Status, data.Message);
		}
	}
}