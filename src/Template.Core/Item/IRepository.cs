using System;
using Template.Core.Base;

namespace Template.Core.Item
{
	public interface IRepository<T> where T : Entity
    {
        T Add(T item);
        Task<List<T>> QueryAsynx(long? lastId = null, int? rows = null, string? order = "asc");
        Task<T?> GetAsync(long id);
        T Update(T item);
        void Remove(T item);
        Task<int> RemoveAsync(long id);
        Task<int> SaveChangesAsync(T item);
    }
}

