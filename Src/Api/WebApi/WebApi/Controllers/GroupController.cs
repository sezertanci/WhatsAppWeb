using Common.Models.RequestModels.GroupRequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ExtendBaseController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateGroupCommand createGroupCommand)
        {
            Guid result = await Mediator.Send(createGroupCommand);

            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateGroupCommand updateGroupCommand)
        {
            bool result = await Mediator.Send(updateGroupCommand);

            return Ok(result);
        }
    }
}
