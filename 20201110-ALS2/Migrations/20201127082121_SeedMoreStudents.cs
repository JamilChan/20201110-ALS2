using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class SeedMoreStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "78b42907-7ab4-4edb-83b4-356ab48c8e46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "950c2352-f18e-4a50-a2bc-ea0f2c4e0a2b", "AQAAAAEAACcQAAAAEA5fE6o3TREFHTryvcHioDkZk1v/HSDEIjV3fzCpT4YycXFSqsphX8naGU3uSpBQNQ==", "b5c681d4-b787-49dd-bea3-efa5d808a50c" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3L,
                column: "EducationId",
                value: 2L);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "EducationId", "Name", "Semester" },
                values: new object[,]
                {
                    { 1L, 1L, "Mathias", 3 },
                    { 2L, 1L, "Hans", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2L);

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

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3L,
                column: "EducationId",
                value: 1L);
        }
    }
}
