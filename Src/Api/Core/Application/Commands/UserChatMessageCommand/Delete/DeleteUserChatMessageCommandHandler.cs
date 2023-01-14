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

        public DeleteUserChatMessageCommandHandler(IUserChatRepository userChatRepository, IUserChatMessageRepository userChatMessageRepository)
        {
            this.userChatRepository = userChatRepository;
            this.userChatMessageRepository = userChatMessageRepository;
        }

        public async Task<bool> Handle(DeleteUserChatMessageCommand request, CancellationToken cancellationToken)
        {
            List<UserChat> userChats = await userChatRepository.GetListAsync(x => x.UserId == request.UserId);

            List<UserChat> friendUserChats = await userChatRepository.GetListAsync(x => x.UserId == request.FriendUserId);

            Guid chatId = userChats.Select(x => x.ChatId).ToList().Intersect(friendUserChats.Select(x => x.ChatId).ToList()).FirstOrDefault();

            List<UserChat> userChatss = await userChatRepository.GetListAsync(x => x.ChatId == chatId, includes: y => y.UserChatMessages);

            List<UserChatMessage> userChatMessages = new();

            foreach(var userChat in userChatss)
            {
                foreach(var userChatMessage in userChat.UserChatMessages.Where(x => x.ShowToUsers != null && x.ShowToUsers.Contains(request.UserId.ToString())))
                {
                    userChatMessage.ShowToUsers = userChatMessage.ShowToUsers.Replace(request.UserId.ToString(), "").Replace(",", "");

                    userChatMessages.Add(userChatMessage);
                }
            }

            if(userChatMessages.Count > 0)
                await userChatMessageRepository.UpdateRangeAsync(userChatMessages);

            return true;
        }
    }
}
