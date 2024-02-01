using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Watch_Data.Migrations
{
    public partial class seedingAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e20b0397-b30f-457c-a489-6cc1297cc8cd", 0, "Le Van Hien", "Da Nang", "568d69b6-0c16-4884-a30b-aa2c38bd9a0d", "byte050403@gmail.com", false, "Hoang", "Truong", false, null, null, null, "AQAAAAEAACcQAAAAED2Exoao4RHEHYn+hY7JuS5tx+qFRd9WcJ3NfzIjf/ReO08oiERsiNCk25urjVTTNQ==", "0981995925", false, "12345", "6e383d61-7d63-40d0-8445-4e566dadf5cb", false, "Hoang Truong" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3f0007dd-8403-4f3a-b5f8-0616bf668d0f", "e20b0397-b30f-457c-a489-6cc1297cc8cd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f0007dd-8403-4f3a-b5f8-0616bf668d0f", "e20b0397-b30f-457c-a489-6cc1297cc8cd" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "e20b0397-b30f-457c-a489-6cc1297cc8cd");
        }
    }
}
