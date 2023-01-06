using Common.Models.ViewModels;

namespace Application.Interfaces.Services
{
    public interface IUserFriendService
    {
        Task<List<UserFriendViewModel>> GetUserFriends(Guid userId);
    }
}
