using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CommunityManagement;
using CMS.Backend.Application.Features.CoursesManagement.Commands;
using CMS.Backend.Application.Features.CoursesManagement.Queries;
using CMS.Backend.Application.Features.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DashboardController : BaseController
    {
        [HttpGet("get-instructor-dashboard")]
        public async Task<IActionResult> GetInstructorDashboard()
            => Ok(await Bus.ExecuteAsync<GetInstructorDashboardQuery, Result>(new GetInstructorDashboardQuery()));

        [HttpGet("get-admin-dashboard")]
        public async Task<IActionResult> GetAdminDashboard()
            => Ok(await Bus.ExecuteAsync<GetAdminDashboardQuery, Result>(new GetAdminDashboardQuery()));

        [HttpGet("get-student-dashboard")]
        public async Task<IActionResult> GetStudentDashboard()
            => Ok(await Bus.ExecuteAsync<GetStudentDashboardQuery, Result>(new GetStudentDashboardQuery()));




    }
}
