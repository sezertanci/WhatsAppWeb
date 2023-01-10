using Common.Models.ViewModels;

namespace Application.Interfaces.Services
{
    public interface IGroupUserService
    {
        Task<List<ChatViewModel>> UserGroups(Guid userId);
    }
}
