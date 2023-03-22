using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Template.Core.Item;

namespace Template.Api.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet("{id}",Name = "GetItem")]
    [ProducesResponseType(typeof(Item),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Item>>> GetItem([FromRoute]long id)
    {
        var item = await _itemRepository.GetAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost(Name = "CreateItem")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status201Created)]
    public async Task<ActionResult<Item>> CreateItem([FromBody] Item item)
    {
        var created = _itemRepository.Add(item);
        await _itemRepository.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItem), new { id = created.Id });
    }

    [HttpPut("{id}", Name = "UpdateItem")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Item>> UpdateItem([FromRoute] long id, [FromBody] Item item)
    {
        var updated = _itemRepository.Update(item);
        if (item == null) return NotFound();
        await _itemRepository.SaveChangesAsync();
        return Ok(updated);
    }

    [HttpDelete("{id}", Name = "DeleteItem")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Item>>> DeleteItem([FromRoute] long id)
    {
        var result = await _itemRepository.RemoveAsync(id);
        if (result == false) return NotFound();
        await _itemRepository.SaveChangesAsync();
        return NoContent();
    }

}

