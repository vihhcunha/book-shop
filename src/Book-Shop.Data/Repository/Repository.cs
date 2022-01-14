using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Models;
using Book_Shop.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book_Shop.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly BookShopContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(BookShopContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(Guid id)
        {
            var entity = new TEntity { Id = id };
            _dbSet.Remove(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           _context?.Dispose();
        }
    }
}
