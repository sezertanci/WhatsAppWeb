using MediatR;

namespace Common.Models.RequestModels.ChatRequestModels
{
    public class DeleteUserChatMessageCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid FriendUserId { get; set; }
        public DeleteUserChatMessageCommand()
        {

        }

        public DeleteUserChatMessageCommand(Guid userId, Guid friendUserId)
        {
            UserId = userId;
            FriendUserId = friendUserId;
        }
    }
}
