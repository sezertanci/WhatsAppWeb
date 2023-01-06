using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class GroupMessageRepository : GenericRepository<GroupMessage>, IGroupMessageRepository
    {
        public GroupMessageRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
