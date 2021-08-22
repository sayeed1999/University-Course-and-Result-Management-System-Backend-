using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixHistoriesTablePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesHistory",
                table: "CoursesHistory");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "StudentsCoursesHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CoursesHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "CoursesHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesHistory",
                table: "CoursesHistory",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesHistory",
                table: "CoursesHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoursesHistory");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CoursesHistory",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCoursesHistories",
                table: "StudentsCoursesHistories",
                columns: new[] { "DepartmentId", "CourseCode", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesHistory",
                table: "CoursesHistory",
                columns: new[] { "Code", "DepartmentId" });
        }
    }
}
