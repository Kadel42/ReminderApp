namespace Reminder.Server.Services.ShoppingListService;

public interface IShoppingListService
{
    Task<ServiceResponse<List<ShoppingList>>> GetShoppingLists();
    Task<ServiceResponse<ShoppingList>> GetShoppingList(int shoppingListId);
    Task<ServiceResponse<ShoppingList>> CreateShoppingList(ShoppingList shoppingList);
    Task<ServiceResponse<ShoppingList>> UpdateShoppingList(ShoppingList shoppingList);
    Task<ServiceResponse<bool>> DeleteShoppingList(int shoppingListId);
    Task<ServiceResponse<ShoppingList>> AddItemToList(int shoppingListId, int shoppingItemId);
    Task<ServiceResponse<ShoppingList>> RemoveItemFromList(int shoppingListId, int shoppingItemId);
}
