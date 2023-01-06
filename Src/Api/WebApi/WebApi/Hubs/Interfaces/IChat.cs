using Common.Models.ViewModels;

namespace WebApi.Hubs.Interfaces
{
    public interface IChat
    {
        Task ChatMessages(List<ChatMessageViewModel> receivedMessageViewModels);
        Task GroupMessages(List<GroupReceivedMessageViewModel> groupReceivedMessageViewModels);
        Task UserFriends(List<UserFriendViewModel> userFriendViewModels);
        Task UserChats(List<ChatViewModel> chatViewModels);
        Task ReceivedMessage(ChatMessageViewModel receivedMessageViewModel);
        Task Refresh();
    }
}
