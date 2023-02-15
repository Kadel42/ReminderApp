using Reminder.Server.Data;
using Reminder.Shared;

namespace Reminder.Server.Services.ShoppingListService;

public class ShoppingListService : IShoppingListService
{
    private readonly DataContext _dataContext;

    public ShoppingListService(DataContext dataContext)
	{
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<ShoppingList>> AddItemToList(int shoppingListId, int shoppingItemId)
    {
        var shoppingItemVariant = await _dataContext.ShoppingItemVariants.FindAsync(shoppingListId, shoppingItemId);
        var shoppingList = await _dataContext.ShoppingLists.FindAsync(shoppingListId);
        var shoppingItem = await _dataContext.ShoppingItems.FindAsync(shoppingItemId);

        if (shoppingList == null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "List not found."
            };
        }

        if (shoppingItem == null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "Item not found."
            };
        }

        if (shoppingItemVariant != null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "Item is already on the list."
            };
        }
        _dataContext.ShoppingItemVariants.Add(new ShoppingItemVariant
        {
            ShoppingItemId = shoppingItemId,
            ShoppingListId = shoppingListId
        });

        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<ShoppingList> { Data = shoppingList };
    }


    public async Task<ServiceResponse<ShoppingList>> CreateShoppingList(ShoppingList shoppingList)
    {
       _dataContext.ShoppingLists.Add(shoppingList);
        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<ShoppingList> { Data= shoppingList };

    }

    public async Task<ServiceResponse<bool>> DeleteShoppingList(int shoppingListId)
    {
        var dbShoppingList = await _dataContext.ShoppingLists.FindAsync(shoppingListId);
        if (dbShoppingList == null)
        {
            return new ServiceResponse<bool> 
            { 
                Data = false,
                Success = false,
                Message = "List not found."

            };
        }

        _dataContext.ShoppingLists.Remove(dbShoppingList);
        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true };
    }

    public async Task<ServiceResponse<ShoppingList>> GetShoppingList(int shoppingListId)
    {
        var response = new ServiceResponse<ShoppingList>();
        ShoppingList shoppingList = new();
        shoppingList = await _dataContext.ShoppingLists
            .Include(l => l.ShoppingItemVariants)
            .ThenInclude(v => v.ShoppingItem)
            .FirstOrDefaultAsync(s => s.Id == shoppingListId && !s.IsSecret);

        if (shoppingList == null)
        {
            response.Success = false;
            response.Message = "List doesn't exist.";
        }
        else
        {
            response.Data = shoppingList;
        }

        return response;
        
    }

    public async Task<ServiceResponse<List<ShoppingList>>> GetShoppingLists()
    {
        var response = new ServiceResponse<List<ShoppingList>>()
        {
            Data = await _dataContext.ShoppingLists.Where(s => !s.IsSecret).ToListAsync() 
        };
        return response;
    }

    public async Task<ServiceResponse<ShoppingList>> RemoveItemFromList(int shoppingListId, int shoppingItemId)
    {
        var shoppingItemVariant = await _dataContext.ShoppingItemVariants.FindAsync(shoppingListId, shoppingItemId);
        var shoppingList = await _dataContext.ShoppingLists.FindAsync(shoppingListId);
        var shoppingItem = await _dataContext.ShoppingItems.FindAsync(shoppingItemId);

        if (shoppingList == null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "List not found."
            };
        }

        if (shoppingItem == null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "Item not found."
            };
        }

        if (shoppingItemVariant == null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "Item is not on the list."
            };
        }
        _dataContext.ShoppingItemVariants.Remove(shoppingItemVariant);

        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<ShoppingList> { Data = shoppingList };
    }

    public async Task<ServiceResponse<ShoppingList>> UpdateShoppingList(ShoppingList shoppingList)
    {
        var dbShoppingList = await _dataContext.ShoppingLists.FindAsync(shoppingList.Id);
        if (dbShoppingList == null)
        {
            return new ServiceResponse<ShoppingList>
            {
                Success = false,
                Message = "List not found."

            };
        }

        dbShoppingList.Name = shoppingList.Name;

        foreach (var variant in shoppingList.ShoppingItemVariants)
        {
            var dbVariant = await _dataContext.ShoppingItemVariants
                .SingleOrDefaultAsync(v => v.ShoppingListId == variant.ShoppingListId
                && v.ShoppingItemId == variant.ShoppingItemId);
            if (dbVariant == null)
            {
                variant.ShoppingItem = null;
                _dataContext.ShoppingItemVariants.Add(variant);
            }
            else
            {
                dbVariant.ShoppingListId = variant.ShoppingListId;
                dbVariant.Bought = variant.Bought;
            }
        }

        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<ShoppingList> { Data = shoppingList };
    }
}
