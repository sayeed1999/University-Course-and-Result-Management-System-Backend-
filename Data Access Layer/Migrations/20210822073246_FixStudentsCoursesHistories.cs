using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixStudentsCoursesHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentsCoursesHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories",
                columns: new[] { "DepartmentId", "CourseCode", "StudentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "StudentsCoursesHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories",
                column: "Id");
        }
    }
}
