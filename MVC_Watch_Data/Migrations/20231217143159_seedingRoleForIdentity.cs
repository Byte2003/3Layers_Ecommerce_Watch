using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Watch_Data.Migrations
{
    public partial class seedingRoleForIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f0007dd-8403-4f3a-b5f8-0616bf668d0f", "3f0007dd-8403-4f3a-b5f8-0616bf668d0f", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4bbab4c-df0d-4621-bf96-5520388de0da", "d4bbab4c-df0d-4621-bf96-5520388de0da", "customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3f0007dd-8403-4f3a-b5f8-0616bf668d0f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d4bbab4c-df0d-4621-bf96-5520388de0da");
        }
    }
}
