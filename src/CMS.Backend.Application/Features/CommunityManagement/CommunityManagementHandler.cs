using CMS.Backend.Appilication.Repository;
using CMS.Backend.Application.Common.Interfaces;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Domain.Entities.CommunityEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.CommunityManagement
{
    public class CommunityManagementHandler :
        IRequestHandler<AddCommunityCommand, Result>,
        IRequestHandler<DeleteCommunityCommand, Result>,
        IRequestHandler<GetInstructorCommunitiesQuery, Result>,
        IRequestHandler<GetStudentCommunitiesQuery, Result>,
        IRequestHandler<GetAllCommunitiesQuery, Result>,
        IRequestHandler<JoinCommunityCommand, Result>,
        IRequestHandler<LeaveCommunityCommand, Result>,
        IRequestHandler<AddPostCommand, Result>,
        IRequestHandler<GetPostsQuery, Result>,
        IRequestHandler<LikePostCommand, Result>,
        IRequestHandler<UpdateCommunityCommand, Result>,
        IRequestHandler<DeletePostCommand, Result>

    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunityRepoistory _communityRepoistory;
        private readonly IUserRepository _userRepository;

        public CommunityManagementHandler(ICommunityRepoistory communityRepoistory, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _communityRepoistory = communityRepoistory;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(AddCommunityCommand request, CancellationToken cancellationToken)
        {
            var owner = Guid.Parse(_currentUserService.UserId!);
            var commuinty = new Community(Guid.NewGuid(), request.Name, request.Subject, owner);
            await _communityRepoistory.AddAsync(commuinty);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            var commuinty = await _communityRepoistory.GetAsync(request.Community);
            commuinty.SetDelete(true);
            await _communityRepoistory.UpdateAsync(commuinty);
            return Result.Success();
        }

        public async Task<Result> Handle(GetInstructorCommunitiesQuery request, CancellationToken cancellationToken)
        {
            var instructor = Guid.Parse(_currentUserService.UserId!);
            var communities = await _communityRepoistory.GetinstructorCommunities(instructor);
            return Result.Success(communities);
        }

        public async Task<Result> Handle(GetAllCommunitiesQuery request, CancellationToken cancellationToken)
        {
            var communities = await _communityRepoistory.GetAllCommunities();
            return Result.Success(communities);
        }

        public async Task<Result> Handle(GetStudentCommunitiesQuery request, CancellationToken cancellationToken)
        {
            var student = Guid.Parse(_currentUserService.UserId!);
            var communities = await _communityRepoistory.GetStudentCommunities(student);
            return Result.Success(communities);
        }

        public async Task<Result> Handle(JoinCommunityCommand request, CancellationToken cancellationToken)
        {
            var member = Guid.Parse(_currentUserService.UserId!);
            var community = await _communityRepoistory.GetAsync(request.Community);
            var userData = await _userRepository.GetAsync(member);
            var newMember = new CommunityMember(member)
            {
                FullName = userData.Name
            };
            if (!community.Members.Any(x => x.Id == member))
            {
                community.Members.Add(newMember);
                await _communityRepoistory.UpdateAsync(community);
            }

            return Result.Success();
        }

        public async Task<Result> Handle(LeaveCommunityCommand request, CancellationToken cancellationToken)
        {
            var member = Guid.Parse(_currentUserService.UserId!);
            var community = await _communityRepoistory.GetAsync(request.Community);
            var selectedMember = community.Members.FirstOrDefault(x => x.Id == member);
            community.Members.Remove(selectedMember!);
            await _communityRepoistory.UpdateAsync(community);
            return Result.Success();
        }

        public async Task<Result> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var member = Guid.Parse(_currentUserService.UserId!);
            var community = await _communityRepoistory.GetAsync(request.Community);
            var post = new CommunityPost(Guid.NewGuid(), request.Content, member);
            community.Posts.Add(post);
            await _communityRepoistory.UpdateAsync(community);
            return Result.Success();
        }

        public async Task<Result> Handle(LikePostCommand request, CancellationToken cancellationToken)
        {
            var community = await _communityRepoistory.GetAsync(request.Community);
            var post = community.Posts.FirstOrDefault(x => x.Id == request.Post);
            post.Likes++;
            await _communityRepoistory.UpdateAsync(community);
            return Result.Success();
        }

        public async Task<Result> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var community = await _communityRepoistory.GetAsync(request.Community);
            var response = new List<object>();
            foreach (var post in community.Posts.Where(x=> !x.IsDeleted))
            {
                var user = await _userRepository.GetAsync(post.Owner);
                response.Add(new
                {
                    name = user.Name,
                    post = post,
                });
            }
            return Result.Success(response);
        }

        public async Task<Result> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = await _communityRepoistory.GetAsync(request.Community);
            community.Name = request.Name;
            community.Subject = request.Subject;
            await _communityRepoistory.UpdateAsync(community);
            return Result.Success();
        }

        public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var community = await _communityRepoistory.GetAsync(request.Community);
            var post = community.Posts.FirstOrDefault(x => x.Id == request.Post);
            post.SetDelete(true);
            await _communityRepoistory.UpdateAsync(community);
            return Result.Success();
        }
    }
}
