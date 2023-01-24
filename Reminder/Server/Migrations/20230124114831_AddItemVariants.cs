using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reminder.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddItemVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItems_ShoppingLists_ShoppingListId",
                table: "ShoppingItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingItems_ShoppingListId",
                table: "ShoppingItems");

            migrationBuilder.DropColumn(
                name: "ShoppingListId",
                table: "ShoppingItems");

            migrationBuilder.CreateTable(
                name: "ShoppingItemVariants",
                columns: table => new
                {
                    ShoppingItemId = table.Column<int>(type: "int", nullable: false),
                    ShoppingListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItemVariants", x => new { x.ShoppingListId, x.ShoppingItemId });
                    table.ForeignKey(
                        name: "FK_ShoppingItemVariants_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingItemVariants");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingListId",
                table: "ShoppingItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ShoppingListId",
                table: "ShoppingItems",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItems_ShoppingLists_ShoppingListId",
                table: "ShoppingItems",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id");
        }
    }
}
