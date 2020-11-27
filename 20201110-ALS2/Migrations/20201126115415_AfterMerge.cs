using Microsoft.EntityFrameworkCore.Migrations;

namespace _20201110_ALS2.Migrations
{
    public partial class AfterMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EducationId",
                table: "Courses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    EducationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.EducationId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2a44267c-c18c-4c14-ad71-d0f1b2ef8c31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6292c1a5-1e73-47fd-b0bb-7d5effb86195", "AQAAAAEAACcQAAAAEISWmt86kkEKCGs/aSLhT8aw7LKR25On+0Es34eIWmtzfWtKk4/B4kdE828YkFaOMw==", "24a61c5d-061d-4281-a4ff-93db729bc22f" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EducationId",
                table: "Courses",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Education_EducationId",
                table: "Courses",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Education_EducationId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropIndex(
                name: "IX_Courses_EducationId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "618707b1-5b97-4d8f-870a-7891a66b6049");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b1eea94-b75b-4a8b-a850-9df5a98fd03b", "AQAAAAEAACcQAAAAENpYDf64J9sxyPkZMTqxAxV3zoGdEn37DK2mkwxqcpFbb7tr3v0FzMaBpk9Or3yJ1Q==", "a9652804-642d-4aa1-b620-d49911b567d0" });
        }
    }
}
