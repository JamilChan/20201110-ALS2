using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class SeedData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Education", "Name", "Semester" },
                values: new object[] { 2L, "Computer Science", "Hans", 3 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Education", "Name", "Semester" },
                values: new object[] { 3L, "Computer Science", "Claus", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3L);
        }
    }
}
