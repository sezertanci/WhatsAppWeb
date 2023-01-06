using MediatR;

namespace Common.Models.RequestModels.UserRequestModels
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public UpdateUserCommand()
        {

        }

        public UpdateUserCommand(Guid id, string phoneNumber, string name, string password)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Name = name;
            Password = password;
        }
    }
}
