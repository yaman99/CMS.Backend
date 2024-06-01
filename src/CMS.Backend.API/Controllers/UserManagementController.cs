using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Appilication.Features.UsersManagement.Queries;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.UsersManagement.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : BaseController
    {
        [HttpPost("add-new-user")]
        public async Task<IActionResult> AddnewUserAsync([FromBody] AddNewUserCommand command)
        {
            var result = await Bus.ExecuteAsync<AddNewUserCommand, Result>(command);

            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }


        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserCommand command)
        {
            var result = await Bus.ExecuteAsync<UpdateUserCommand, Result>(command);

            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await Bus.ExecuteAsync<GetAllUsersQuery, Result>(new GetAllUsersQuery());

            if (result.Succeeded)
                return Ok(result.Data);
            return BadRequest(result.Errors);
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUserAsync()
        {
             return Ok(await Bus.ExecuteAsync<GetUserQuery, Result>(new GetUserQuery()));
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUserAsync([FromBody] DeleteUserCommand command)
        {
            var result = await Bus.ExecuteAsync<DeleteUserCommand, Result>(command);

            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Errors);
        }
    }
}
