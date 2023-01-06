using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UserFriendRepository : GenericRepository<UserFriend>, IUserFriendRepository
    {
        public UserFriendRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
