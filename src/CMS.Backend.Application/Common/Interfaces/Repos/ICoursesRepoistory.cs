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
        Task<Course> GetAsync(Guid id);
        Task UpdateAsync(Course course);
        Task<IEnumerable<Course>> GetinstructorCourses(Guid instructor);
        Task<IEnumerable<Course>> GetStudentCourses(Guid student);
        Task<IEnumerable<Course>> GetAllCourses(bool cond,Guid student);
    }
}
