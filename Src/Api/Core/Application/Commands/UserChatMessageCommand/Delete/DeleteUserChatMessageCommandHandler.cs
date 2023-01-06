using Application.Interfaces.Repositories;
using Common.Models.RequestModels.ChatRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserChatMessageCommand.Delete
{
    public class DeleteUserChatMessageCommandHandler : IRequestHandler<DeleteUserChatMessageCommand, bool>
    {
        private readonly IUserChatRepository userChatRepository;
        private readonly IUserChatMessageRepository userChatMessageRepository;
        private readonly IChatRepository chatRepository;

        public DeleteUserChatMessageCommandHandler(IUserChatRepository userChatRepository, IUserChatMessageRepository userChatMessageRepository, IChatRepository chatRepository)
        {
            this.userChatRepository = userChatRepository;
            this.userChatMessageRepository = userChatMessageRepository;
            this.chatRepository = chatRepository;
        }

        public async Task<bool> Handle(DeleteUserChatMessageCommand request, CancellationToken cancellationToken)
        {
            List<UserChat> userChats = await userChatRepository.GetListAsync(x => x.UserId == request.UserId);

            List<UserChat> friendUserChats = await userChatRepository.GetListAsync(x => x.UserId == request.FriendUserId);

            Guid chatId = userChats.Select(x => x.ChatId).ToList().Intersect(friendUserChats.Select(x => x.ChatId).ToList()).FirstOrDefault();

            UserChat userChat = await userChatRepository.GetFirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == request.UserId, includes: y => y.UserChatMessages);

            List<UserChatMessage> userChatMessages = new();

            foreach (var userChatMessage in userChat.UserChatMessages.Where(x => !x.Deleted))
            {
                userChatMessage.Deleted = true;
                userChatMessage.DeletedDate = DateTime.Now;

                userChatMessages.Add(userChatMessage);
            }

            if (userChatMessages.Count > 0)
                await userChatMessageRepository.UpdateRangeAsync(userChatMessages);

            return true;
        }
    }
}
