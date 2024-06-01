using CMS.Backend.Appilication.Repository;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Domain.Entities.CommunityEntity;
using CMS.Backend.Domain.Entities.CommunityEntity;
using CMS.Backend.Infrastructure.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Infrastructure.Repositories
{
    public class CommunityRepoistory : ICommunityRepoistory
    {
        private readonly IMongoRepository<Community, Guid> _repository;

        public CommunityRepoistory(IMongoRepository<Community, Guid> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Community Community)
        {
            await _repository.AddAsync(Community);
        }

        public async Task<IEnumerable<Community>> GetAllCommunities()
        {
            return await _repository.FindAsync(x=> !x.IsDeleted);
        }

        public Task<Community> GetAsync(Guid id)
        {
            return _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Community>> GetinstructorCommunities(Guid instructor)
        {
            return await _repository.FindAsync(x => x.Owner == instructor && !x.IsDeleted);
        }

        public async Task<IEnumerable<Community>> GetStudentCommunities(Guid student)
        {
            return await _repository.FindAsync(x => x.Members.Any(t => t.Id == student) && !x.IsDeleted);
        }

        public async Task UpdateAsync(Community Community)
        {
            await _repository.UpdateAsync(Community);
        }
    }

}
