using MediatR;

namespace Common.Models.RequestModels.ChatRequestModels
{
    public class DeleteGroupMessageCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DeleteGroupMessageCommand()
        {

        }

        public DeleteGroupMessageCommand(Guid userId, Guid groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }
    }
}
