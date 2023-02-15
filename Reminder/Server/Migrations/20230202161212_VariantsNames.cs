using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reminder.Server.Migrations
{
    /// <inheritdoc />
    public partial class VariantsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItemVariants_ShoppingItemId",
                table: "ShoppingItemVariants",
                column: "ShoppingItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItemVariants_ShoppingItems_ShoppingItemId",
                table: "ShoppingItemVariants",
                column: "ShoppingItemId",
                principalTable: "ShoppingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItemVariants_ShoppingItems_ShoppingItemId",
                table: "ShoppingItemVariants");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingItemVariants_ShoppingItemId",
                table: "ShoppingItemVariants");
        }
    }
}
