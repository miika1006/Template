using System;
namespace Template.Core.Item
{
	public interface IItemRepository
	{
        Task<List<Item>> QueryItems(string? searchWord = null, long? lastId = null, int? rows = null, string? order = "asc");
	}
}

