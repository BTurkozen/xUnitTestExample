using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using xUnitTestExample.Web.Models;

namespace xUnitTestExample.Web.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;

            await _dataContext.SaveChangesAsync();
        }
    }
}
