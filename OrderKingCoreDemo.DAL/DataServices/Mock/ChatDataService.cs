using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices.Mock {
	public class ChatDataService : BaseMockDataService, IChatDataService {
		public Task<RequestResult> SendMessage(ChatMessageObject message, string token, CancellationToken cts) {
			return new Task<RequestResult>(() => new RequestResult(RequestStatus.Ok));
		}

		public Task<RequestResult<List<ChatMessageObject>>> GetMessages(string token, CancellationToken cts) {
			return GetMockDataList<ChatMessageObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Chat.ChatMessages.json");
		}

	    public Task<RequestResult<List<ChatCategoryObject>>> GetCategories(string hostelId, string token, CancellationToken cts)
	    {
			return GetMockDataList<ChatCategoryObject>(@"OrderKingCoreDemo.DAL.Resources.Mock.Chat.ChatCategories.json");
		}
	}
}