using Microsoft.AspNetCore.Mvc;
using Template.Core.Item;

namespace Template.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly IItemRepository _itemRepository;

    public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository)
    {
        _logger = logger;
        _itemRepository = itemRepository;
    }

    [HttpGet(Name = "GetItems")]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        return await _itemRepository.QueryItemsAsync();
    }
}

