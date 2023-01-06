using Common.Models.RequestModels.ChatRequestModels;
using Common.Models.RequestModels.UserChatMessageRequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChatMessageController : ExtendBaseController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserChatMessageCommand createUserChatMessageCommand)
        {
            Guid result = await Mediator.Send(createUserChatMessageCommand);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserChatMessageCommand deleteChatCommand)
        {
            bool result = await Mediator.Send(deleteChatCommand);

            return Ok(result);
        }
    }
}
