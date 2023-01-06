using MediatR;

namespace Common.Models.RequestModels.UserChatMessageRequestModels
{
    public class CreateUserChatMessageCommand : IRequest<Guid>
    {
        public Guid UserChatId { get; set; }
        public string Message { get; set; }

        public CreateUserChatMessageCommand()
        {

        }

        public CreateUserChatMessageCommand(Guid userChatId, string message)
        {
            UserChatId = userChatId;
            Message = message;
        }
    }
}
