using Common.Models.RequestModels.UserRequestModels;
using WebAppMvc.Infrastructure.Services.Interfaces;

namespace WebAppMvc.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public Task<bool> Login(LoginUserCommand loginUserCommand)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Register(CreateUserCommand createUserCommand)
        {
            var response = await client.PostAsJsonAsync("User/Create", createUserCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }
    }
}
