using CMS.Backend.Appilication.Repository;
using CMS.Backend.Application.Common.Interfaces;
using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.Dashboard
{
    public class DashboardHandler :
        IRequestHandler<GetInstructorDashboardQuery, Result>,
        IRequestHandler<GetAdminDashboardQuery, Result>,
        IRequestHandler<GetStudentDashboardQuery, Result>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunityRepoistory _communityRepoistory;
        private readonly IUserRepository _userRepository;
        private readonly ICoursesRepoistory _coursesRepository;

        public DashboardHandler(ICurrentUserService currentUserService, ICommunityRepoistory communityRepoistory, IUserRepository userRepository, ICoursesRepoistory coursesRepository)
        {
            _currentUserService = currentUserService;
            _communityRepoistory = communityRepoistory;
            _userRepository = userRepository;
            _coursesRepository = coursesRepository;
        }

        public async Task<Result> Handle(GetInstructorDashboardQuery request, CancellationToken cancellationToken)
        {
            var instructorId = Guid.Parse(_currentUserService.UserId!);
            var communities = await _communityRepoistory.GetinstructorCommunities(instructorId);
            var courses = await _coursesRepository.GetinstructorCourses(instructorId);

            var result = new
            {
                TotalStudent = courses.Select(x => x.EnrolledStudents).Count(),
                TotalCourses = courses.Count(),
                TotalCommunities = communities.Count(),
            };

            return Result.Success(result);

        }

        public async Task<Result> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            var communities = await _communityRepoistory.GetAllCommunities();
            var courses = await _coursesRepository.GetAllCourses();
            var users = await _userRepository.GetAllAsync();

            var result = new
            {
                TotalStudents = users.Count(x => x.UserType == "student"),
                TotalInstructors = users.Count(x => x.UserType == "instructor"),
                TotalCourses = courses.Count(),
                TotalCommunities = communities.Count(),
            };
            return Result.Success(result);

        }

        public async Task<Result> Handle(GetStudentDashboardQuery request, CancellationToken cancellationToken)
        {
            var studentId = Guid.Parse(_currentUserService.UserId!);
            var communities = await _communityRepoistory.GetStudentCommunities(studentId);
            var courses = await _coursesRepository.GetStudentCourses(studentId);

            var result = new
            {
                TotalCourses = courses.Count(),
                TotalCommunities = communities.Count(),
            };
            return Result.Success(result);
        }
    }
}
