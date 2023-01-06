using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(WhatsAppWebContext context) : base(context)
        {
        }
    }
}
