using System;
using Microsoft.EntityFrameworkCore;
using Template.Core.Base;
using Template.Core.Item;

namespace Template.Infrastructure.Data.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;
        public Repository(string connectionString)
        {
            _context = new ApplicationDbContext(connectionString);
        }
        public virtual T Add(T item)
        {
            _context.Set<T>().Add(item);
            return item;
        }
        public virtual Task<List<T>> QueryAscendingAsync(long? lastId = null, int? rows = null)
        {
            return QueryAsync(lastId, rows, "asc");
        }
        public virtual Task<List<T>> QueryDescendingAsync(long? lastId = null, int? rows = null)
        {
            return QueryAsync(lastId, rows, "desc");
        }
        private async Task<List<T>> QueryAsync(long? lastId = null, int? rows = null, string? order = "asc")
        {
            var query = _context.Set<T>().AsQueryable();
            bool ascending = order?.ToLower() == "asc";
            if (lastId.HasValue) query = ascending ? query.Where(i => i.Id > lastId) : query.Where(i => i.Id < lastId);
            var orderedQuery = ascending ? query.OrderBy(i => i.Id) : query.OrderByDescending(i => i.Id);
            return rows.HasValue ?
                    await orderedQuery.Take(rows.Value).ToListAsync() :
                    await orderedQuery.ToListAsync();

        }
        public virtual async Task<T?> GetAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual T? Update(T item)
        {
            if (_context.Set<T>().Any(i => i.Id == item.Id)) _context.Set<T>().Update(item);
            else return null;

            return item;
        }
        public virtual void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }
        public virtual async Task<bool> RemoveAsync(long id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                Remove(item);
                return true;
            }
            return false;
        }
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

