using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixingHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NthHistory",
                table: "StudentsCoursesHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NthHistory",
                table: "CoursesHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NthHistory",
                table: "AllocateClassroomHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NthHistory",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "NthHistory",
                table: "CoursesHistory");

            migrationBuilder.DropColumn(
                name: "NthHistory",
                table: "AllocateClassroomHistories");
        }
    }
}
