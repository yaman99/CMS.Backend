﻿using CMS.Backend.Application.Common.Models;
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
        public bool ForExplore { get; set; }
    }
    public class GetInstructorCoursesQuery : IRequest<Result>
    {
    }
    public class GetStudentCoursesQuery : IRequest<Result>
    {
    }
}
