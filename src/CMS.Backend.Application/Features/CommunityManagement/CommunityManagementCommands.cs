using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.CommunityManagement
{
    public class AddCommunityCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
    }
    public class DeleteCommunityCommand : IRequest<Result>
    {
        public Guid Community { get; set; }
    }

    public class JoinCommunityCommand : IRequest<Result>
    {
        public Guid Community { get; set; }
    }
    public class LeaveCommunityCommand : IRequest<Result>
    {
        public Guid Community { get; set; }
    }
    public class AddPostCommand : IRequest<Result>
    {
        public string Content { get; set; }
        public Guid Community { get; set; }
    }
    public class LikePostCommand : IRequest<Result>
    {
        public Guid Post { get; set; }
        public Guid Community { get; set; }
    }
}
