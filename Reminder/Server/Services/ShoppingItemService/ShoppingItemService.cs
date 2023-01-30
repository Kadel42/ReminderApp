using Microsoft.EntityFrameworkCore;
using Reminder.Server.Data;
using Reminder.Shared;

namespace Reminder.Server.Services.ShoppingItemService;

public class ShoppingItemService : IShoppingItemService
{
    private readonly DataContext _dataContext;

    public ShoppingItemService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<ShoppingItem>> CreateItem(ShoppingItem shoppingItem)
    {
        _dataContext.ShoppingItems.Add(shoppingItem);
        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<ShoppingItem> { Data = shoppingItem };
    }

    public async Task<ServiceResponse<bool>> DeleteItem(int shoppingItemId)
    {
        var dbShoppingItem = await _dataContext.ShoppingItems.FindAsync(shoppingItemId);
        if (dbShoppingItem == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = "Item not found."

            };
        }

        _dataContext.ShoppingItems.Remove(dbShoppingItem);
        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true };
    }

    public async Task<ServiceResponse<List<ShoppingItem>>> GetAllItems()
    {
        var response = new ServiceResponse<List<ShoppingItem>>()
        {
            Data = await _dataContext.ShoppingItems.ToListAsync()
        };
        return response;
    }

    public async Task<ServiceResponse<ShoppingItem>> GetItem(int shoppingItemId)
    {
        var shoppingItem = await _dataContext.ShoppingItems.FirstOrDefaultAsync(i => i.Id == shoppingItemId);
        if (shoppingItem == null)
        {
            return new ServiceResponse<ShoppingItem>
            {
                Message = "Item not found.",
                Success = false
            };
        }
        
        return new ServiceResponse<ShoppingItem>(){ Data = shoppingItem }; 
    }

    public async Task<ServiceResponse<List<ShoppingItem>>> GetItemsByList(int shoppingListId)
    {
        var variantsInList = await _dataContext.ShoppingItemVariants.Where(v 
            => v.ShoppingListId == shoppingListId).ToListAsync();
        var shoppingItems = new List<ShoppingItem>();

        if (variantsInList.Count == 0)
        {
            return new ServiceResponse<List<ShoppingItem>>
            {
                Success = false,
                Message = "There are no items on this list."
            };
        }

        foreach (var variant in variantsInList)
        {
            
            var shoppingItem = await _dataContext.ShoppingItems.Where(i 
                => i.Id == variant.ShoppingItemId).FirstOrDefaultAsync();
            if (shoppingItem != null)
            {
                shoppingItems.Add(shoppingItem);
            }
            
        }

        if (shoppingItems.Count == 0)
        {
            return new ServiceResponse<List<ShoppingItem>>
            {
                Success = false,
                Message = "No items found."
            };
        }

        return new ServiceResponse<List<ShoppingItem>> { Data = shoppingItems };
    }

    public async Task<ServiceResponse<List<ShoppingItem>>> GetItemsNotOnList(int shoppingListId)
    {
        var variantsInList = await _dataContext.ShoppingItemVariants.Where(v
            => v.ShoppingListId == shoppingListId).ToListAsync();
        var shoppingItems = await _dataContext.ShoppingItems.ToListAsync();

        if (variantsInList.Count > 0)
        {
            foreach (var variant in variantsInList)
            {

                var shoppingItem = await _dataContext.ShoppingItems.Where(i
                    => i.Id == variant.ShoppingItemId).FirstOrDefaultAsync();
                if (shoppingItem != null)
                {
                    shoppingItems.Remove(shoppingItem);
                }
            }
        }

        if (shoppingItems.Count == 0)
        {
            return new ServiceResponse<List<ShoppingItem>>
            {
                Success = false,
                Message = "All existing items are on the list."
            };
        }

        return new ServiceResponse<List<ShoppingItem>> { Data = shoppingItems };
    }

    public async Task<ServiceResponse<ShoppingItem>> UpdateItem(ShoppingItem shoppingItem)
    {
        var dbShoppingItem = await _dataContext.ShoppingLists.FindAsync(shoppingItem.Id);
        if (dbShoppingItem == null)
        {
            return new ServiceResponse<ShoppingItem>
            {
                Success = false,
                Message = "Item not found."

            };
        }

        dbShoppingItem.Name = shoppingItem.Name;

        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<ShoppingItem> { Data = shoppingItem };
    }
}
