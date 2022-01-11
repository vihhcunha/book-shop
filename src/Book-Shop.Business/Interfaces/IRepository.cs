using Book_Shop.Business.Models;
using System.Linq.Expressions;

namespace Book_Shop.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task Remove(Guid id);
        Task Update(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
