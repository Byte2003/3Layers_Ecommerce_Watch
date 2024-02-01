using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Watch_Data.Migrations
{
    public partial class fixStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_Products_ProductID",
                table: "ProductDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Products_ProductID",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDiscount",
                table: "ProductDiscount");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "ProductDiscount",
                newName: "ProductsDiscount");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_ProductID",
                table: "Stocks",
                newName: "IX_Stocks_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscount_ProductID",
                table: "ProductsDiscount",
                newName: "IX_ProductsDiscount_ProductID");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsDiscount",
                table: "ProductsDiscount",
                column: "PDiscountID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsDiscount_Products_ProductID",
                table: "ProductsDiscount",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductID",
                table: "Stocks",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsDiscount_Products_ProductID",
                table: "ProductsDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductID",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsDiscount",
                table: "ProductsDiscount");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "ProductsDiscount",
                newName: "ProductDiscount");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ProductID",
                table: "Stock",
                newName: "IX_Stock_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsDiscount_ProductID",
                table: "ProductDiscount",
                newName: "IX_ProductDiscount_ProductID");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDiscount",
                table: "ProductDiscount",
                column: "PDiscountID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_Products_ProductID",
                table: "ProductDiscount",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Products_ProductID",
                table: "Stock",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
