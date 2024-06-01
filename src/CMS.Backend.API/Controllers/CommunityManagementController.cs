using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CommunityManagement;
using CMS.Backend.Application.Features.CoursesManagement.Commands;
using CMS.Backend.Application.Features.CoursesManagement.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CommunityManagementController : BaseController
    {
        [HttpPost("add-community")]
        public async Task<IActionResult> AddCourse([FromBody] AddCommunityCommand command)
            => Ok(await Bus.ExecuteAsync<AddCommunityCommand, Result>(command));

        [HttpPost("delete-community")]
        public async Task<IActionResult> DeleteCommunity([FromBody] DeleteCommunityCommand command)
            => Ok(await Bus.ExecuteAsync<DeleteCommunityCommand, Result>(command));

        [HttpGet("get-instructor-communities")]
        public async Task<IActionResult> GetInstructorCommunities()
            => Ok(await Bus.ExecuteAsync<GetInstructorCommunitiesQuery, Result>(new GetInstructorCommunitiesQuery()));

        [HttpGet("get-student-communities")]
        public async Task<IActionResult> GetStudentCommunities()
            => Ok(await Bus.ExecuteAsync<GetStudentCommunitiesQuery, Result>(new GetStudentCommunitiesQuery()));


        [HttpGet("get-communities")]
        public async Task<IActionResult> GetAllCommunities()
            => Ok(await Bus.ExecuteAsync<GetAllCommunitiesQuery, Result>(new GetAllCommunitiesQuery()));

        [HttpPost("join-community")]
        public async Task<IActionResult> JoinCommunity([FromBody] JoinCommunityCommand command)
            => Ok(await Bus.ExecuteAsync<JoinCommunityCommand, Result>(command));

        [HttpPost("leave-community")]
        public async Task<IActionResult> LeaveCommunity([FromBody] LeaveCommunityCommand command)
            => Ok(await Bus.ExecuteAsync<LeaveCommunityCommand, Result>(command));

        [HttpPost("add-post")]
        public async Task<IActionResult> AddPost([FromBody] AddPostCommand command)
            => Ok(await Bus.ExecuteAsync<AddPostCommand, Result>(command));
        [HttpPost("like-post")]
        public async Task<IActionResult> LikePost([FromBody] LikePostCommand command)
            => Ok(await Bus.ExecuteAsync<LikePostCommand, Result>(command));

        [HttpGet("get-posts/{id}")]
        public async Task<IActionResult> GetPosts(Guid id)
            => Ok(await Bus.ExecuteAsync<GetPostsQuery, Result>(new GetPostsQuery
            {
                Community = id
            }));




    }
}
