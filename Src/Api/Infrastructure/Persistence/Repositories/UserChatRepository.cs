using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UserChatRepository : GenericRepository<UserChat>, IUserChatRepository
    {
        public UserChatRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
