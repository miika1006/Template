using System;
using Microsoft.EntityFrameworkCore;
using Template.Core.Base;

namespace Template.Infrastructure.Data.Repositories.Base
{
    public abstract class Repository<T> where T : Entity
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
        public async Task<List<T>> Query(long? lastId = null, int? rows = null, string? order = "asc")
        {
            var query = _context.Set<T>().AsQueryable();
            bool ascending = order?.ToLower() == "asc";
            if (lastId.HasValue) query = ascending ? query.Where(i => i.Id > lastId) : query.Where(i => i.Id < lastId);
            var orderedQuery = ascending ? query.OrderBy(i => i.Id) : query.OrderByDescending(i => i.Id);
            return rows.HasValue ?
                    await orderedQuery.Take(rows.Value).ToListAsync() :
                    await orderedQuery.ToListAsync();

        }
        public virtual async Task<T?> Get(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual T Update(T item)
        {
             _context.Set<T>().Update(item);
            return item;
        }
        public virtual void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }
        public async Task<int> RemoveAsync(long id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                Remove(item);
                return 1;
            }
            return 0;
        }
        public virtual async Task<int> SaveChangesAsync(T item)
        {
            return await _context.SaveChangesAsync();
        }
    }
}

