using Common.Models.ViewModels;

namespace Application.Interfaces.Services
{
    public interface IUserChatService
    {
        Task<List<ChatViewModel>> UserChats(Guid userId);
    }
}
