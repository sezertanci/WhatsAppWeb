using MediatR;

namespace Common.Models.RequestModels.UserRequestModels
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public CreateUserCommand()
        {

        }

        public CreateUserCommand(string phoneNumber, string name, string password)
        {
            PhoneNumber = phoneNumber;
            Name = name;
            Password = password;
        }
    }
}
