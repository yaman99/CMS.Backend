using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.CommunityManagement
{
    public class GetInstructorCommunitiesQuery : IRequest<Result>
    {
    }
    public class GetStudentCommunitiesQuery : IRequest<Result>
    {
    }
    public class GetAllCommunitiesQuery : IRequest<Result>
    {
    }
    public class GetPostsQuery : IRequest<Result>
    {
        public Guid Community { get; set; }
    }
}
