using CMS.Backend.Appilication.Repository;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Domain.Entities.CourseEntity;
using CMS.Backend.Infrastructure.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Infrastructure.Repositories
{
    public class CoursesRepoistory : ICoursesRepoistory
    {
        private readonly IMongoRepository<Course, Guid> _repository;

        public CoursesRepoistory(IMongoRepository<Course, Guid> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Course course)
        {
            await _repository.AddAsync(course);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _repository.FindAsync(x=> !x.IsDeleted);
        }

        public Task<Course> GetAsync(Guid id)
        {
            return _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Course>> GetinstructorCourses(Guid instructor)
        {
            return await _repository.FindAsync(x => x.Instructor == instructor && !x.IsDeleted);
        }

        public async Task<IEnumerable<Course>> GetStudentCourses(Guid student)
        {
            return await _repository.FindAsync(x => x.EnrolledStudents.Any(t => t.Id == student));
        }

        public async Task UpdateAsync(Course course)
        {
            await _repository.UpdateAsync(course);
        }
    }

}
