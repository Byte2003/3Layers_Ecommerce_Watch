using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Watch_Data.Migrations
{
    public partial class addProductStockDiscountToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StockID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProductDiscount",
                columns: table => new
                {
                    PDiscountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscount", x => x.PDiscountID);
                    table.ForeignKey(
                        name: "FK_ProductDiscount_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        //onDelete: ReferentialAction.Cascade);
			            onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockID);
                    table.ForeignKey(
                        name: "FK_Stock_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockID",
                table: "Products",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_ProductID",
                table: "ProductDiscount",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductID",
                table: "Stock",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stock_StockID",
                table: "Products",
                column: "StockID",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stock_StockID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductDiscount");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockID",
                table: "Products");
        }
    }
}
