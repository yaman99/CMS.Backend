using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using CMS.Backend.Domain.Common;
using CMS.Backend.Shared.Types;

namespace CMS.Backend.Infrastructure.Mongo
{
    public interface IMongoRepository<TEntity,T> where TEntity : IIdentifiable<T>
    {
        IMongoCollection<TEntity> Collection { get; }
         Task<TEntity> GetAsync(T id);
         Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
         Task<IList<TEntity>> GetAll();
         Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
         Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
				TQuery query) where TQuery : PagedQueryBase;
         Task AddAsync(TEntity entity);
         Task UpdateAsync(TEntity entity);
         Task DeleteAsync(T id); 
         Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}