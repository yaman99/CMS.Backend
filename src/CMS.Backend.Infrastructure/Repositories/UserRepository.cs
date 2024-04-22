using CMS.Backend.Appilication.Repository;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Infrastructure.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User, Guid> _repository;

        public UserRepository(IMongoRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckEmailExist(string email)
            => await _repository.ExistsAsync(x => x.Email == email && !x.IsDeleted);


        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(x=>x.Id == id && !x.IsDeleted);

        public async Task<User> GetAsync(string email)
            => await _repository.GetAsync(x => x.Email == email.ToLowerInvariant() );

        public async Task AddAsync(User user)
            => await _repository.AddAsync(user);

        public async Task UpdateAsync(User user)
            => await _repository.UpdateAsync(user);


        public async Task<IEnumerable<User>> GetAllAsync()
            => await _repository.FindAsync(x => !x.IsDeleted);
        
    }

}
