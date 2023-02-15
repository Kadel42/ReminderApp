using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reminder.Shared;
public class ShoppingItemVariant
{
    
    public ShoppingItem? ShoppingItem { get; set; }
    public int ShoppingItemId { get; set; }
    [JsonIgnore]
    public ShoppingList? ShoppingList { get; set; }
    public int ShoppingListId { get; set; }
    public bool Bought { get; set; } = false;
}
