using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Watch_Data.Migrations
{
    public partial class updateFKForStockAndDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stock_StockID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Stock_ProductID",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockID",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductID",
                table: "Stock",
                column: "ProductID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_ProductID",
                table: "Stock");

            migrationBuilder.AddColumn<Guid>(
                name: "StockID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductID",
                table: "Stock",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockID",
                table: "Products",
                column: "StockID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stock_StockID",
                table: "Products",
                column: "StockID",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
