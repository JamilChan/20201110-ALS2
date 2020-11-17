using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class Intial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Education", "Name", "Semester" },
                values: new object[] { 1L, "Computer Science", "Mathias", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1L);
        }
    }
}
