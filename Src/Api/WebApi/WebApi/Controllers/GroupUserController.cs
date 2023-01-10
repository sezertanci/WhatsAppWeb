using Application.Queries;
using Common.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController : ExtendBaseController
    {

        [HttpGet("GetList/{groupId}")]
        public async Task<IActionResult> GetList(Guid groupId)
        {
            List<GetUsersViewModel> result = await Mediator.Send(new GetGroupUsersQuery(groupId));

            return Ok(result);
        }
    }
}
