using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices {
	public interface IProfileDataService
	{
		Task<RequestResult<ProfileObject>> Login(string password, string token, CancellationToken cts);
		Task<RequestResult<ProfileObject>> Register(ProfileObject userInfo, string token, CancellationToken cts);
		Task<RequestResult<string>> GetPassword(string phone, CancellationToken cts);
	    Task<RequestResult> CancelPlacement(string token, CancellationToken cts);
	    Task<RequestResult<ProfileObject>> ChangeUserInfo(ProfileObject newUserInfo, string token, CancellationToken cts);
	    Task<RequestResult<ProfileObject>> GetUserInfo(string token, CancellationToken cts);
	    Task<RequestResult<List<NotificationObject>>> GetAllNotification(string token, CancellationToken cts);
	    Task<RequestResult<NotificationObject>> GetNotification(string notifyId, string token, CancellationToken cts);
	    Task<RequestResult<List<OrderHistoryObject>>> GetOrderHistoryList(string token, CancellationToken cts);
	}
}