namespace Reminder.Server.Services.ShoppingItemService;

public interface IShoppingItemService
{
    Task<ServiceResponse<List<ShoppingItem>>> GetAllItems();
    Task<ServiceResponse<List<ShoppingItem>>> GetItemsByList(int shoppingListId);
    Task<ServiceResponse<List<ShoppingItem>>> GetItemsNotOnList(int shoppingListId);
    Task<ServiceResponse<List<ShoppingItemVariant>>> GetVariantsOnList(int shoppingListId);
    Task<ServiceResponse<ShoppingItem>> GetItem(int shoppingItemId);
    Task<ServiceResponse<ShoppingItem>> CreateItem(ShoppingItem shoppingItem);
    Task<ServiceResponse<ShoppingItem>> UpdateItem(ShoppingItem shoppingItem);
    Task<ServiceResponse<bool>> DeleteItem(int shoppingItemId);
}
