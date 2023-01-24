using Microsoft.EntityFrameworkCore;
using Reminder.Shared;

namespace Reminder.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingItemVariant>().HasKey(siv => new { siv.ShoppingListId, siv.ShoppingItemId });
    }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<ShoppingItem> ShoppingItems { get; set; }
    public DbSet<ShoppingItemVariant> ShoppingItemVariants { get; set;}
}
