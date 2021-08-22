using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixCourseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "CoursesHistory");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CoursesHistory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CoursesHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Credit",
                table: "CoursesHistory",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CoursesHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CoursesHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
