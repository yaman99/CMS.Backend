using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesManagementController : BaseController
    {
        [HttpPost("add-course")]
        public async Task<IActionResult> AddCourse([FromBody] AddNewUserCommand command)
            => Ok(await Bus.ExecuteAsync<AddNewUserCommand, Result>(command));
        //[HttpPost("add-course")]
        //public async Task<IActionResult> UpdateCourse([FromBody] AddNewUserCommand command)
        //    => Ok(await Bus.ExecuteAsync<AddNewUserCommand, Result>(command));
        //[HttpPost("add-course")]
        //public async Task<IActionResult> DeleteCourse([FromBody] AddNewUserCommand command)
        //    => Ok(await Bus.ExecuteAsync<AddNewUserCommand, Result>(command));

    }
}
