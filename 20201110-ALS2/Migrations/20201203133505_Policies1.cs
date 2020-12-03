using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class Policies1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16b29b3e-912f-45b8-8eba-9078acc912c0");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e9a33f0e-dbc7-4b90-bb60-b3dc1679f6a4" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9a33f0e-dbc7-4b90-bb60-b3dc1679f6a4");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 7, "Giv Fravær", "Giv Fravær", "1" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8190fe4f-e42d-4639-8cd9-e521afec759b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12808a43-7649-42ae-b9f5-38e2c73dacaf", "9a7e30d8-bc08-4109-b3b9-5bc282cc142e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "EducatorId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1fea0eaa-bffe-49d3-8f75-fb3f78712ac2", 0, "dc812ea8-66c9-493e-adb4-c2d2a1e9c407", 1L, null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEMss5txAzsMmj4GpxG8QW+mFiNS6YbQt+RIeTJvfhpHdI+anR+ZgwoWKNesfxRS4cw==", null, false, "5c089318-8c72-4393-a0b9-ac8554e5c1d3", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1fea0eaa-bffe-49d3-8f75-fb3f78712ac2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12808a43-7649-42ae-b9f5-38e2c73dacaf");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1fea0eaa-bffe-49d3-8f75-fb3f78712ac2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1fea0eaa-bffe-49d3-8f75-fb3f78712ac2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5517f152-57c2-4625-9a49-65a533b03cbf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16b29b3e-912f-45b8-8eba-9078acc912c0", "e282fd73-e8a1-4a99-8530-0b268c4bdb57", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "EducatorId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e9a33f0e-dbc7-4b90-bb60-b3dc1679f6a4", 0, "10cddffd-ceb5-4807-92be-1de25cb3b1e7", 1L, null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEK93AOtnvBYEZQn3jyZSYFmWfb4zCQAY1204Ldk+laFBafNjbuqgLG/INy+ghXfT5g==", null, false, "e587a4af-8982-4a39-8f77-9454965a63b7", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e9a33f0e-dbc7-4b90-bb60-b3dc1679f6a4" });
        }
    }
}
