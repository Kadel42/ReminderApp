using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reminder.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShoppingItemController : ControllerBase
{
    private readonly IShoppingItemService _shoppingItemService;

    public ShoppingItemController(IShoppingItemService shoppingItemService)
	{
        _shoppingItemService = shoppingItemService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ShoppingItem>>>> GetAllItems()
    {
        var result = await _shoppingItemService.GetAllItems();

        return Ok(result);
    }

    [HttpGet("onlist/{listId}")]
    public async Task<ActionResult<ServiceResponse<List<ShoppingItem>>>> GetItemsByList(int listId)
    {
        var result = await _shoppingItemService.GetItemsByList(listId);

        return Ok(result);
    }

    [HttpGet("notonlist/{listId}")]
    public async Task<ActionResult<ServiceResponse<List<ShoppingItem>>>> GetItemsNotOnList(int listId)
    {
        var result = await _shoppingItemService.GetItemsNotOnList(listId);

        return Ok(result);
    }

    [HttpGet("variants/{listId}")]
    public async Task<ActionResult<ServiceResponse<List<ShoppingItemVariant>>>> GetVariantsOnList(int listId)
    {
        var result = await _shoppingItemService.GetVariantsOnList(listId);

        return Ok(result);
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<ServiceResponse<ShoppingItem>>> GetItem(int itemId)
    {
        var result = await _shoppingItemService.GetItem(itemId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<ShoppingItem>>> CreateItem(ShoppingItem item)
    {
        var result = await _shoppingItemService.CreateItem(item);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<ShoppingItem>>> UpdateItem(ShoppingItem shoppingItem)
    {
        Console.WriteLine("controller");
        var result = await _shoppingItemService.UpdateItem(shoppingItem);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteItem(int id)
    {
        var result = await _shoppingItemService.DeleteItem(id);

        return Ok(result);
    }
}
