using Reminder.Shared;

namespace Reminder.Client.Services;

public interface IShoppingItemService
{
    List<ShoppingItem> ShoppingItems { get; set; }
    string Message { get; set; }
    Task<ShoppingItem> CreateItem(ShoppingItem shoppingItem);
    Task<ServiceResponse<ShoppingItem>> GetItem(int shoppingItemId);
    Task<ShoppingItem> UpdateItem(ShoppingItem shoppingItem);
    Task DeleteItem(ShoppingItem shoppingItem);
    Task GetAllItems();
    Task GetItemsByList(int shoppingListId);
    Task GetItemsNotOnList(int shoppingListId);
}
