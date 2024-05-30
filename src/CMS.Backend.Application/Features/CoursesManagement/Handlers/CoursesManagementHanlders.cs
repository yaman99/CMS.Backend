using AutoMapper;
using CMS.Backend.Appilication.Repository;
using CMS.Backend.Application.Common.Exceptions;
using CMS.Backend.Application.Common.Interfaces;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CoursesManagement.Commands;
using CMS.Backend.Application.Features.CoursesManagement.Queries;
using CMS.Backend.Domain.Entities;
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
        IRequestHandler<GetStudentCoursesQuery, Result>,
        IRequestHandler<GetLessonsQuery, Result>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICoursesRepoistory _coursesRepository;
        private readonly IMapper _mapper;

        public CoursesManagementHanlders(ICurrentUserService currentUserService, ICoursesRepoistory coursesRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var instructorId = Guid.Parse(_currentUserService.UserId!);
            var courrse = new Course(Guid.NewGuid() ,instructorId, request.Title , request.Subject , request.Description);

            await _coursesRepository.AddAsync(courrse);
            return Result.Success();
        }

        public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetAsync(request.Id);
            var updatedCourse = _mapper.Map(request, course);
            await _coursesRepository.UpdateAsync(updatedCourse);
            return Result.Success();
        }

        public Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Handle(AddLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetAsync(request.Course);


            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var videoName = Guid.NewGuid().ToString("N") + Path.GetExtension(request.Video.FileName);
            var filePath = Path.Combine(uploadsFolder, videoName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.Video.CopyToAsync(fileStream);
            }

            var lesson = new Lesson(Guid.NewGuid());
            lesson.Title = request.Title;
            lesson.Video = videoName;
            lesson.Size = request.Video.Length;
            lesson.Description = request.Description;
            course.Lessons.Add(lesson);
            await _coursesRepository.UpdateAsync(course);

            return Result.Success();
        }

        public async Task<Result> Handle(EditLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetAsync(request.Course);
            var lesson = course.Lessons.FirstOrDefault(x => x.Id == request.Lesson);
            if (lesson == null)
            {
                throw new NotFoundException();
            }
            lesson.Title = request.Title;
            lesson.Description= request.Description;
            await _coursesRepository.UpdateAsync(course);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetAsync(request.Course);
            var lesson = course.Lessons.FirstOrDefault(x=> x.Id == request.Lesson);
            if (lesson == null)
            {
                throw new NotFoundException();
            }
            course.Lessons.Remove(lesson);
            await _coursesRepository.UpdateAsync(course);
            return Result.Success();
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

        public async Task<Result> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
        {
            var data = await _coursesRepository.GetAsync(request.CourseId);
            return Result.Success(data.Lessons);
        }
    }
}
