using System.Windows.Input;

namespace OrderKingCoreDemo.BL.ViewModels.Chat
{
    public class ChatCategoriesViewModel: BaseViewModel {
	    public ICommand GoToChatCommand => MakeNavigateToCommand(Pages.Chat);
    }
}
