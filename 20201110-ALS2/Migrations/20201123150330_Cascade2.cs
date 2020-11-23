using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class Cascade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Courses_CourseId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Courses_CourseId",
                table: "Absences",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Courses_CourseId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Courses_CourseId",
                table: "Absences",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
