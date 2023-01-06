using Application.Queries;
using Common.Models.Queries;
using Common.Models.RequestModels.UserRequestModels;
using Common.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ExtendBaseController
    {
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            List<GetUsersViewModel> result = await Mediator.Send(new GetUsersQuery());

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            Guid result = await Mediator.Send(createUserCommand);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            bool result = await Mediator.Send(updateUserCommand);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            LoginUserViewModel result = await Mediator.Send(loginUserCommand);

            return Ok(result);
        }
    }
}
