using Common.Models.RequestModels.ChatRequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMessageController : ExtendBaseController
    {
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGroupMessageCommand deleteGroupMessageCommand)
        {
            bool result = await Mediator.Send(deleteGroupMessageCommand);

            return Ok(result);
        }
    }
}
