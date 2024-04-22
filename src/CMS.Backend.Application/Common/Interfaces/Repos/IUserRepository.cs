using CMS.Backend.Domain;
using CMS.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Repository
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExist(string email);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
