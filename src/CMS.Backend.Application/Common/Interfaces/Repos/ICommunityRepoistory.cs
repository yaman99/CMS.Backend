using CMS.Backend.Domain;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Domain.Entities.CommunityEntity;
using CMS.Backend.Domain.Entities.CourseEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Repository
{
    public interface ICommunityRepoistory
    {
        Task AddAsync(Community Community);
        Task<Community> GetAsync(Guid id);
        Task UpdateAsync(Community Community);
        Task<IEnumerable<Community>> GetinstructorCommunities(Guid instructor);
        Task<IEnumerable<Community>> GetStudentCommunities(Guid student);
        Task<IEnumerable<Community>> GetAllCommunities();

    }
}
