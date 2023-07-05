using Hrm.Domain.Abstractions;
using System.Linq.Expressions;

namespace Hrm.Application.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByKeyAsync<TKey>(TKey key);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
        Task<int> SaveChangesAsync();
        Task<bool> ChangeDbConnectionString(string connectionString);
        Task CreateDatabase();
    }
}
