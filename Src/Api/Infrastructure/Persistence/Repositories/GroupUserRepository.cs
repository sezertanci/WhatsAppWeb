using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class GroupUserRepository : GenericRepository<GroupUser>, IGroupUserRepository
    {
        public GroupUserRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
