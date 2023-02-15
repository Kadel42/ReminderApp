using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Shared;
public class ShoppingItem
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [NotMapped]
    public bool Editing { get; set; } = false;
}
