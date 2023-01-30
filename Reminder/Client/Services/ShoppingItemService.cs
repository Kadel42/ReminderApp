using System.Net.Http.Json;

namespace Reminder.Client.Services;

public class ShoppingItemService : IShoppingItemService
{
    private readonly HttpClient _httpClient;

    public ShoppingItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<ShoppingItem> ShoppingItems { get; set; } = new();
    public string Message { get; set; } = "Loading items...";

    public async Task<ShoppingItem> CreateItem(ShoppingItem shoppingItem)
    {
        var result = await _httpClient.PostAsJsonAsync("api/shoppingitem", shoppingItem);
        var newItem = (await result.Content.ReadFromJsonAsync<ServiceResponse<ShoppingItem>>()).Data;
        return newItem;
    }

    public async Task DeleteItem(ShoppingItem shoppingItem)
    {
        var result = await _httpClient.DeleteAsync($"api/shoppingitem/{shoppingItem.Id}");
    }

    public async Task GetAllItems()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ShoppingItem>>>("api/shoppingitem");

        if (result != null && result.Data != null)
        {
            ShoppingItems = result.Data;
        }

        if (ShoppingItems.Count == 0)
        {
            Message = "No items found.";
        }
    }

    public async Task<ServiceResponse<ShoppingItem>> GetItem(int shoppingItemId)
    {
        var result = 
            await _httpClient.GetFromJsonAsync<ServiceResponse<ShoppingItem>>($"api/shoppingitem/{shoppingItemId}");

        if (result == null)
        {
            return new ServiceResponse<ShoppingItem> { Success = false, Message = "Item not found." };
        }

        return result;
    }

    public async Task GetItemsByList(int shoppingListId)
    {
        var result = 
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<ShoppingItem>>>($"api/shoppingitem/onlist/{shoppingListId}");

        if (result != null && result.Data != null)
        {
            ShoppingItems = result.Data;
        }

        if (ShoppingItems.Count == 0)
        {
            Message = "No items found.";
        }
    }

    public async Task GetItemsNotOnList(int shoppingListId)
    {
        var result =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<ShoppingItem>>>($"api/shoppingitem/notonlist/{shoppingListId}");

        if (result != null && result.Data != null)
        {
            ShoppingItems = result.Data;
        }

        if (ShoppingItems.Count == 0)
        {
            Message = "No items found.";
        }
    }

    public async Task<ShoppingItem> UpdateItem(ShoppingItem shoppingItem)
    {
        var result = await _httpClient.PutAsJsonAsync(@"api/shoppingitem", shoppingItem);
        return (await result.Content.ReadFromJsonAsync<ServiceResponse<ShoppingItem>>()).Data;
    }
}
