using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UserChatMessageRepository : GenericRepository<UserChatMessage>, IUserChatMessageRepository
    {
        public UserChatMessageRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
