using CMS.Backend.Application.Common.Mappings;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Domain.Entities.CourseEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.CoursesManagement.Commands
{
    public class AddCourseCommand : IRequest<Result>
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
    public class UpdateCourseCommand : IRequest<Result> , IMapFrom<Course>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class DeleteCourseCommand : IRequest<Result>
    {
        public Guid CourseId { get; set; }
    }

    public class ApplyOnCourseCommand : IRequest<Result>
    {
        public Guid CourseId { get; set; }
    }
    public class AddLessonCommand : IRequest<Result>
    {
        public Guid Course { get; set; }
        public string Title { get; set; }
        public IFormFile Video { get; set; }
        public string Description { get; set; }
    }
    public class EditLessonCommand : IRequest<Result>
    {
        public Guid Lesson { get; set; }
        public Guid Course { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class DeleteLessonCommand : IRequest<Result>
    {
        public Guid Lesson { get; set; }
        public Guid Course { get; set; }
    }

    public class GetLessonsQuery : IRequest<Result>
    {
        public Guid CourseId { get; set; }
    }
}
