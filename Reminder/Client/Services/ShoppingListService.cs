using System.Net.Http.Json;

namespace Reminder.Client.Services;

public class ShoppingListService : IShoppingListService
{
    private readonly HttpClient _httpClient;

    public ShoppingListService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<ShoppingList> ShoppingLists { get; set; } = new();
    public string Message { get; set; } = "Loading lists...";

    public async Task AddItemToList(int shoppingListId, ShoppingItem shoppingItem)
    {
        await _httpClient.PostAsJsonAsync($"api/shoppinglist/additem/{shoppingListId}/{shoppingItem.Id}", shoppingItem);
    }

    public async Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList)
    {
        var result = await _httpClient.PostAsJsonAsync("api/shoppinglist", shoppingList);
        var newList = (await result.Content.ReadFromJsonAsync<ServiceResponse<ShoppingList>>()).Data;
        return newList;
    }

    public async Task DeleteShoppingList(ShoppingList shoppingList)
    {
        var result = await _httpClient.DeleteAsync($"api/shoppinglist/{shoppingList.Id}");
    }

    public async Task<ServiceResponse<ShoppingList>> GetShoppingList(int shoppingListId)
    {
        var result = 
            await _httpClient.GetFromJsonAsync<ServiceResponse<ShoppingList>>($"api/shoppinglist/{shoppingListId}");

        if (result == null)
        {
            return new ServiceResponse<ShoppingList> { Success = false, Message = "List not found" };
        }

        return result;
    }

    public async Task GetShoppingLists()
    {
        var result = 
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<ShoppingList>>>("api/shoppinglist");

        if (result != null && result.Data != null)
        {
            ShoppingLists = result.Data;
        }

        if (ShoppingLists.Count == 0)
        {
            Message = "No lists found.";
        }

    }

    public async Task RemoveItemFromList(int shoppingListId, int shoppingItemId)
    {
        await _httpClient.DeleteAsync($"api/shoppinglist/removeitem/{shoppingListId}/{shoppingItemId}");
    }

    public async Task<ShoppingList> UpdateShoppingList(ShoppingList shoppingList)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/shoppinglist", shoppingList);
        return (await result.Content.ReadFromJsonAsync<ServiceResponse<ShoppingList>>()).Data;
    }
}
