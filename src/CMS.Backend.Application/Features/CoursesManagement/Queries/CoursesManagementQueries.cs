using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.CoursesManagement.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.CoursesManagement.Queries
{
    public class GetCoursesQuery : IRequest<Result>
    {
        
    }
    public class GetInstructorCoursesQuery : IRequest<Result>
    {
        public Guid Instructor { get; set; }
    }
    public class GetStudentCoursesQuery : IRequest<Result>
    {
        public Guid Student { get; set; }
    }
}
