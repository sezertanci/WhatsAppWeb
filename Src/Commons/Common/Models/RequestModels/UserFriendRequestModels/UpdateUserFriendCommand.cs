using MediatR;

namespace Common.Models.RequestModels.UserFriendRequestModels
{
    public class UpdateUserFriendCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public UpdateUserFriendCommand()
        {

        }

        public UpdateUserFriendCommand(Guid id, Guid userId, string name, string phoneNumber)
        {
            Id = id;
            UserId = userId;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
