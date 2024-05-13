using CMS.Backend.Application.Common.Models;
using MediatR;
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
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
    public class UpdateCourseCommand : IRequest<Result>
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
    public class DeleteCourseCommand : IRequest<Result>
    {
        public Guid CourseId { get; set; }
    }
    public class AddLesson : IRequest<Result>
    {
        public string Name { get; set; }
        public IFileInfo Video { get; set; }
        public string Description { get; set; }
    }
    public class EditLesson : IRequest<Result>
    {
        public string Name { get; set; }
        public IFileInfo Video { get; set; }
        public string Description { get; set; }
    }
    public class DeleteLesson : IRequest<Result>
    {
        public string Name { get; set; }
        public IFileInfo Video { get; set; }
        public string Description { get; set; }
    }
}
