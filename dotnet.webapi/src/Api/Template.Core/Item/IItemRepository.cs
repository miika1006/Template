using System;
namespace Template.Core.Item
{
	public interface IItemRepository : IRepository<Item>
	{
        Task<List<Item>> QueryItemsAsync(string? searchWord = null, long? lastId = null, int? rows = null, string? order = "asc");
	}
}

