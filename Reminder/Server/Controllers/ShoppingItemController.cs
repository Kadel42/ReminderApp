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

    [HttpGet("{itemId}")]
    public async Task<ActionResult<ServiceResponse<ShoppingItem>>> GetItem(int itemId)
    {
        var result = await _shoppingItemService.GetItem(itemId);

        return Ok(result);
    }
}
