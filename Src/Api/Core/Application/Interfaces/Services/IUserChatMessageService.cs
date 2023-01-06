using Common.Models.ViewModels;

namespace Application.Interfaces.Services
{
    public interface IUserChatMessageService
    {
        Task<ChatMessageViewModel> SendMessageAsync(SendedMessageViewModel sendedMessageViewModel);
        Task<List<ChatMessageViewModel>> GetChatMessagesAsync(Guid userId, Guid friendUserId);
        Task HasBeenReadChatMessagesAsync(Guid userId, Guid friendUserId);

    }
}
