using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoturWebApp.Migrations
{
    public partial class BasketProductIdRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Products_ProductID",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ProductID",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Basket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Basket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ProductID",
                table: "Basket",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Products_ProductID",
                table: "Basket",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID");
        }
    }
}
