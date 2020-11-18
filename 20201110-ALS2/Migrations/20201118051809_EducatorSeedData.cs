using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class EducatorSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Educators",
                columns: table => new
                {
                    EducatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educators", x => x.EducatorId);
                });

            migrationBuilder.InsertData(
                table: "Educators",
                columns: new[] { "EducatorId", "Name" },
                values: new object[] { 1, "God Flemse" });

            migrationBuilder.InsertData(
                table: "Educators",
                columns: new[] { "EducatorId", "Name" },
                values: new object[] { 2, "Big Daddy D" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educators");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Students");
        }
    }
}
