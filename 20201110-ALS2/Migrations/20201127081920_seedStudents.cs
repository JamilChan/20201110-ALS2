using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class seedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3e505e76-d030-42a5-8596-2ee060604bbc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac1c2d63-4151-416d-a5b0-97a77b5904d6", "AQAAAAEAACcQAAAAEGgasU94Va+g9UlzUKvx9z58Do4t4rj/27edj/NuUvzjf5LR9rd57mTC+++m7Ag3Lw==", "52567ad8-852c-4dcb-9a46-e8664383b330" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "EducationId", "Name", "Semester" },
                values: new object[] { 3L, 1L, "Claus", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9a1ecc21-6f65-445a-a60c-9ef588e975c5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ad1d883-2efe-4853-9606-9e5261caf346", "AQAAAAEAACcQAAAAEOvdr93NUE8g15dtI9aehOG+ZCV+Mm/sBodBGOUujjEI7glF6oZRV96uNb9TaGL78g==", "f05ec5e9-5c2a-4d4d-aca4-375aebc545e4" });
        }
    }
}
