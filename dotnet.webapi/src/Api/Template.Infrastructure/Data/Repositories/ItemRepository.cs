using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Template.Core.Item;
using Template.Infrastructure.Data.Repositories.Base;

namespace Template.Infrastructure.Data.Repositories
{
	public class ItemRepository : Repository<Item>, IItemRepository
    {
		public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

        
        public async Task<List<Item>> QueryItemsAsync(string? searchWord = null, long? lastId = null, int? rows = null, string? order = "asc")
        {
            var query = _context.Items.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchWord)) query = query.Where(i => i.Name.Contains(searchWord));
            bool ascending = order?.ToLower() == "asc";
            if (lastId.HasValue) query = ascending ? query.Where(i => i.Id > lastId) : query.Where(i => i.Id < lastId);
            var orderedQuery = ascending ? query.OrderBy(i => i.Id) : query.OrderByDescending(i => i.Id);
            return rows.HasValue ?
                    await orderedQuery.Take(rows.Value).ToListAsync() :
                    await orderedQuery.ToListAsync();

        }
    }
}

