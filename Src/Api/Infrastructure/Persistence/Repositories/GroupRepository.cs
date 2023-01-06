using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
