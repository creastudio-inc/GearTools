using System.Linq.Expressions;

namespace AOEntityFramework.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool isDescending = false);

        Task<IEnumerable<TEntity>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null, bool isDescending = false);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy = null, bool isDescending = false);

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        // Méthodes supplémentaires
        Task UpdatePartialAsync(Guid id, object valuesToUpdate);

        Task<TEntity> GetByIdAsync(params object[] keyValues);

        Task DeleteAsync(Guid id);

        Task RestoreAsync(Guid id);

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}