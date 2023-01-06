using MediatR;

namespace Common.Models.RequestModels.UserFriendRequestModels
{
    public class CreateUserFriendCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public CreateUserFriendCommand()
        {

        }

        public CreateUserFriendCommand(Guid userId, string name, string phoneNumber)
        {
            UserId = userId;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
