using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Common.Models.ViewModels;
using Domain.Models;

namespace Persistence.Menagers
{
    public class UserFriendManager : IUserFriendService
    {
        private readonly IUserRepository userRepository;

        public UserFriendManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<UserFriendViewModel>> GetUserFriends(Guid userId)
        {
            User user = await userRepository.GetFirstOrDefaultAsync(x => x.Id == userId, includes: y => y.UserFriends);

            List<User> users = await userRepository.GetListAsync(x => !x.Deleted);

            List<UserFriendViewModel> userFriendViewModels = user.UserFriends.Select(x => new UserFriendViewModel
            {
                UserId = users.FirstOrDefault(y => y.PhoneNumber == x.PhoneNumber)?.Id,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
            }).ToList();

            return userFriendViewModels;
        }
    }
}
