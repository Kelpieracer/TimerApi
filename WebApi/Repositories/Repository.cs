using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        public IQueryable<TEntity> GetAll();
        public Task<TEntity> GetByIdAsync(int id);
        public Task<TEntity> AddAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<TEntity> DeleteAsync(int id, int accountId);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                var items = _context.Set<TEntity>();
                return items;
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                var items = _context.Set<TEntity>();
                var item = await items.FindAsync(id);
                if(item == null)
                    throw new Exception("Couldn't find the entity");
                return item;
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve the entity");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be saved");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be updated");
            }
        }

        public async Task<TEntity> DeleteAsync(int id, int accountId)
        {
            TEntity entity;
            try
            {
                entity = await _context.FindAsync<TEntity>(id);
                if(entity == null)
                    throw new AppException($"{nameof(entity)} was not found.");

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception err)
            {
                throw new AppException($"{nameof(entity)} could not be deleted. {err.Message}");
            }
        }
    }
}
