using Common.Models.ViewModels;
using MediatR;

namespace Common.Models.RequestModels.UserRequestModels
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public LoginUserCommand()
        {

        }

        public LoginUserCommand(string phoneNumber, string password)
        {
            PhoneNumber = phoneNumber;
            Password = password;
        }
    }
}
