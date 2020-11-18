using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class EducatorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Educators_EducatorId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "EducatorId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Educators_EducatorId",
                table: "Courses",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "EducatorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Educators_EducatorId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "EducatorId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Educators_EducatorId",
                table: "Courses",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "EducatorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
