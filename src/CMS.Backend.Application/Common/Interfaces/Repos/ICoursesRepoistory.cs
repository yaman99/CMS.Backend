using CMS.Backend.Domain;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Domain.Entities.CourseEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Repository
{
    public interface ICoursesRepoistory
    {
        Task AddAsync(Course course);
        Task<IEnumerable<Course>> GetinstructorCourses(Guid instructor);
    }
}
