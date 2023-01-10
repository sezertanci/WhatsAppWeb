using Application.Queries;
using Common.Models.Queries;
using Common.Models.RequestModels.UserFriendRequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFriendController : ExtendBaseController
    {
        [HttpGet("GetFriends/{userId}")]
        public async Task<IActionResult> GetFriends(Guid userId)
        {
            List<GetUsersViewModel> result = await Mediator.Send(new GetUserFriendsQuery(userId));

            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserFriendCommand createUserFriendCommand)
        {
            Guid result = await Mediator.Send(createUserFriendCommand);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserFriendCommand updateUserFriendCommand)
        {
            bool result = await Mediator.Send(updateUserFriendCommand);

            return Ok(result);
        }
    }
}
