using Common.Models.RequestModels.UserRequestModels;

namespace WebAppMvc.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(LoginUserCommand loginUserCommand);
        Task<Guid> Register(CreateUserCommand createUserCommand);
    }
}
