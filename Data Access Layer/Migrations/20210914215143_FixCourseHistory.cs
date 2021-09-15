using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixCourseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "CoursesHistories");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "CoursesHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistories_CourseId",
                table: "CoursesHistories",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesHistories_Courses_CourseId",
                table: "CoursesHistories",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesHistories_Courses_CourseId",
                table: "CoursesHistories");

            migrationBuilder.DropIndex(
                name: "IX_CoursesHistories_CourseId",
                table: "CoursesHistories");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CoursesHistories");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CoursesHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
