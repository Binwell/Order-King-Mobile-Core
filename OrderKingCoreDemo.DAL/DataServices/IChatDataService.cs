using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices
{
    public interface IChatDataService
    {
		Task<RequestResult> SendMessage(ChatMessageObject message, string token, CancellationToken cts);
        Task<RequestResult<List<ChatMessageObject>>> GetMessages(string token,  CancellationToken cts);
        Task<RequestResult<List<ChatCategoryObject>>> GetCategories(string hostelId, string token, CancellationToken cts);
    }
}