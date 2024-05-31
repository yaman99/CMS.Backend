using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CoursesManagement.Commands;
using CMS.Backend.Application.Features.CoursesManagement.Queries;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost("apply-on-course")]
        public async Task<IActionResult> ApplyOnCourse([FromBody] ApplyOnCourseCommand command)
            => Ok(await Bus.ExecuteAsync<ApplyOnCourseCommand, Result>(command));

        //Lessons

        [HttpPost("add-lesson")]
        public async Task<IActionResult> AddLesson([FromForm] AddLessonCommand command)
            => Ok(await Bus.ExecuteAsync<AddLessonCommand, Result>(command));
        [HttpPost("delete-lesson")]
        public async Task<IActionResult> DeleteLesson([FromBody] DeleteLessonCommand command)
            => Ok(await Bus.ExecuteAsync<DeleteLessonCommand, Result>(command));
        [HttpPost("edit-lesson")]
        public async Task<IActionResult> EditLesson([FromBody] EditLessonCommand command)
            => Ok(await Bus.ExecuteAsync<EditLessonCommand, Result>(command));

        [HttpGet("get-lessons/{id}")]
        public async Task<IActionResult> GetLessons(Guid id)
            => Ok(await Bus.ExecuteAsync<GetLessonsQuery, Result>(new GetLessonsQuery { CourseId = id}));




        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
           => Ok(await Bus.ExecuteAsync<GetCoursesQuery, Result>(new GetCoursesQuery()));

        [HttpGet("get-instructor-courses")]
        public async Task<IActionResult> GetInstructorCourses()
           => Ok(await Bus.ExecuteAsync<GetInstructorCoursesQuery, Result>(new GetInstructorCoursesQuery()));

        [HttpGet("get-student-courses")]
        public async Task<IActionResult> GetStudentCourses()
           => Ok(await Bus.ExecuteAsync<GetStudentCoursesQuery, Result>(new GetStudentCoursesQuery()));

    }
}
