using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class WeekAddedFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Weeks_WeekId",
                table: "Courses");

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Weeks_WeekId",
                table: "Courses",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "WeekId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Weeks_WeekId",
                table: "Courses");

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "Courses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Weeks_WeekId",
                table: "Courses",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "WeekId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
