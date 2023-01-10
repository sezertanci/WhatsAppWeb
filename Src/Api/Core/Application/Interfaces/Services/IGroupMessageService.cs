using Common.Models.ViewModels;

namespace Application.Interfaces.Services
{
    public interface IGroupMessageService
    {
        Task<GroupMessageViewModel> SendMessageAsync(SendedGroupMessageViewModel sendedGroupMessageViewModel);
        Task<List<ChatMessageViewModel>> GetChatMessagesAsync(Guid userId, Guid groupId);
    }
}
