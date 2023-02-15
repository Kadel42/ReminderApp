using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Shared;
public class ShoppingList
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public List<ShoppingItemVariant> ShoppingItemVariants { get; set; } = new();
    public bool IsSecret { get; set; } = false;
    [NotMapped]
    public bool Editing { get; set; } = false;
}
