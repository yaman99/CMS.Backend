using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CoursesManagement.Commands;
using CMS.Backend.Application.Features.CoursesManagement.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesManagementController : BaseController
    {
        [HttpPost("add-course")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseCommand command)
            => Ok(await Bus.ExecuteAsync<AddCourseCommand, Result>(command));

        [HttpPost("update-course")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
            => Ok(await Bus.ExecuteAsync<UpdateCourseCommand, Result>(command));

        [HttpPost("delete-course")]
        public async Task<IActionResult> DeleteCourse([FromBody] DeleteCourseCommand command)
            => Ok(await Bus.ExecuteAsync<DeleteCourseCommand, Result>(command));

        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
           => Ok(await Bus.ExecuteAsync<GetCoursesQuery, Result>(new GetCoursesQuery()));

        [HttpGet("get-instructor-courses/{id}")]
        public async Task<IActionResult> GetInstructorCourses(Guid id)
           => Ok(await Bus.ExecuteAsync<GetInstructorCoursesQuery, Result>(new GetInstructorCoursesQuery
           {
               Instructor = id
           }));

        [HttpGet("get-student-courses/{id}")]
        public async Task<IActionResult> GetStudentCourses(Guid id)
           => Ok(await Bus.ExecuteAsync<GetStudentCoursesQuery, Result>(new GetStudentCoursesQuery
           {
               Student = id
           }));

    }
}
