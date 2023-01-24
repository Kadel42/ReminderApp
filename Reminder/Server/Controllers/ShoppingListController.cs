using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reminder.Shared;

namespace Reminder.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShoppingListController : ControllerBase
{
    private readonly IShoppingListService _shoppingListService;

    public ShoppingListController(IShoppingListService shoppingListService)
	{
        _shoppingListService = shoppingListService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ShoppingList>>>> GetShoppingLists()
    {
        var result = await _shoppingListService.GetShoppingLists();
        return Ok(result);
    }

    [HttpGet("{shoppingListId}")]
    public async Task<ActionResult<ServiceResponse<ShoppingList>>> GetShoppingList(int shoppingListId)
    {
        var result = await _shoppingListService.GetShoppingList(shoppingListId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteShoppingList(int id)
    {
        var result = await _shoppingListService.DeleteShoppingList(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<ShoppingList>>> CreateShoppingList(ShoppingList shoppingList)
    {
        var result = await _shoppingListService.CreateShoppingList(shoppingList);
        return Ok(result);
    }
}
