using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class Policies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd807ff1-7ce5-43c3-afd0-ffcf504fe603");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "b1d273b1-3e62-4562-a24b-90a75e0765ab" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b1d273b1-3e62-4562-a24b-90a75e0765ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Håndter Studerende", "Håndter Studerende" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Se Studerende", "Se Studerende" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Slet Studerende", "Slet Studerende" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Håndter Fag", "Håndter Fag" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Se Fag", "Se Fag" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Slet Fag", "Slet Fag" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Håndter Rolle", "Håndter Rolle" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Se Roller", "Se Roller" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Slet Rolle", "Slet Rolle" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Håndter Undervisere", "Håndter Undervisere" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Se Undervisere", "Se Undervisere" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Slet Undervisere", "Slet Undervisere" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 7, "Håndter Studerende", "Håndter Studerende", "1" },
                    { 8, "Se Studerende", "Se Studerende", "1" },
                    { 9, "Slet Studerende", "Slet Studerende", "1" },
                    { 10, "Håndter Fag", "Håndter Fag", "1" },
                    { 11, "Se Fag", "Se Fag", "1" },
                    { 12, "Slet Fag", "Slet Fag", "1" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "09df0135-1c6c-45fa-b874-2cc3b11e8e94");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd807ff1-7ce5-43c3-afd0-ffcf504fe603", "4f47ea70-e2fc-41de-bc46-54de66b57e80", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "EducatorId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b1d273b1-3e62-4562-a24b-90a75e0765ab", 0, "d692aa0a-42e5-4404-8703-a4c4dbe013d5", 1L, null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEFrK7XRgHvj1koRAEU2fyylPZWgF35JNezifrDNwvckSe728Xflfc2xOv37PXPSu1w==", null, false, "596089cf-db99-4b4e-a2b7-d5fb7fa92ec7", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "b1d273b1-3e62-4562-a24b-90a75e0765ab" });
        }
    }
}
