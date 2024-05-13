using CMS.Backend.Appilication.Repository;
using CMS.Backend.Application.Common.Interfaces;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CoursesManagement.Commands;
using CMS.Backend.Application.Features.CoursesManagement.Queries;
using CMS.Backend.Domain.Entities.CourseEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.CoursesManagement.Handlers
{
    public class CoursesManagementHanlders :
        IRequestHandler<AddCourseCommand, Result>,
        IRequestHandler<UpdateCourseCommand, Result>,
        IRequestHandler<DeleteCourseCommand, Result>,
        IRequestHandler<AddLessonCommand, Result>,
        IRequestHandler<EditLessonCommand, Result>,
        IRequestHandler<DeleteLessonCommand, Result>,
        IRequestHandler<GetCoursesQuery, Result>,
        IRequestHandler<GetInstructorCoursesQuery, Result>,
        IRequestHandler<GetStudentCoursesQuery, Result>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICoursesRepoistory _coursesRepository;

        public CoursesManagementHanlders(ICurrentUserService currentUserService, ICoursesRepoistory coursesRepository)
        {
            _currentUserService = currentUserService;
            _coursesRepository = coursesRepository;
        }

        public async Task<Result> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var instructorId = Guid.Parse(_currentUserService.UserId!);
            var courrse = new Course(Guid.NewGuid() ,instructorId, request.Title , request.Subject , request.Description);

            await _coursesRepository.AddAsync(courrse);
            return Result.Success();
        }

        public Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Handle(AddLessonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Handle(EditLessonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Handle(GetInstructorCoursesQuery request, CancellationToken cancellationToken)
        {
            var instructorId = Guid.Parse(_currentUserService.UserId!);
            var data = await _coursesRepository.GetinstructorCourses(instructorId);
            return Result.Success(data);
        }

        public Task<Result> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
