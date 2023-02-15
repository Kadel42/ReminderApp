namespace Reminder.Client.Services;

public interface IShoppingListService
{
    List<ShoppingList> ShoppingLists { get; set; }
    List<ShoppingItemVariant> BoughtItems { get; set; }
    List<ShoppingItemVariant> ItemsToBuy { get; set; }
    string Message { get; set; }
    Task GetShoppingLists();
    Task<ServiceResponse<ShoppingList>> GetShoppingList(int shoppingListId);
    Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList);
    Task<ShoppingList> UpdateShoppingList(ShoppingList shoppingList);
    Task DeleteShoppingList(ShoppingList shoppingList);
    Task AddItemToList(int shoppingListId, ShoppingItem shoppingItem);
    Task RemoveItemFromList(int shoppingListId, int shoppingItemId);
}
