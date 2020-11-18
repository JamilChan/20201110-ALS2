using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class AdminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "460049b4-4ef2-4117-94ae-a514eeabed8d", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEKRRQeFNLDiVnH8v8YUEupluvGvrwd7x8hxa3iM67duk4rghzkImxJuPIOW6YdQ5HA==", null, false, "6c82b5cb-039b-4ded-b24f-f825cc5cd14b", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
