using System;
using Template.Core.Base;

namespace Template.Core.Item
{
	public interface IRepository<T> where T : Entity
    {
        T Add(T item);
        Task<List<T>> QueryAscendingAsync(long? lastId = null, int? rows = null);
        Task<List<T>> QueryDescendingAsync(long? lastId = null, int? rows = null);
        Task<T?> GetAsync(long id);
        T? Update(T item);
        void Remove(T item);
        Task<bool> RemoveAsync(long id);
        Task<int> SaveChangesAsync();
    }
}

