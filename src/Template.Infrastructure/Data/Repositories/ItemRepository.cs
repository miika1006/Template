using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Template.Core.Item;
using Template.Infrastructure.Data.Repositories.Base;

namespace Template.Infrastructure.Data.Repositories
{
	public class ItemRepository : Repository<Item>, IItemRepository
    {
		public ItemRepository(string connectionString) : base(connectionString)
		{
		}

        public async Task<List<Item>> QueryItems(string? searchWord = null, long? lastId = null, int? rows = null)
        {
            var query = _context.Items.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchWord)) query = query.Where(i => i.Name.Contains(searchWord));
            if (lastId.HasValue) query = query.Where(i => i.Id > lastId);
            
            return rows.HasValue ? await query.OrderBy(i => i.Id).Take(rows.Value).ToListAsync() : await query.OrderBy(i => i.Id).ToListAsync();

        }
    }
}

