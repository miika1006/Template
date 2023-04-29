using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Template.Core.Item;

namespace Template.Api.V2.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
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
    [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Item>>> GetItems()
    {
        return Ok(await _itemRepository.QueryItemsAsync());
    }

}

