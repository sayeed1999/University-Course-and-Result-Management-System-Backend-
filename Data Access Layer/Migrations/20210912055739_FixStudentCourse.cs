using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixStudentCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_DepartmentId",
                table: "StudentsCourses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Departments_DepartmentId",
                table: "StudentsCourses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Departments_DepartmentId",
                table: "StudentsCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentsCourses_DepartmentId",
                table: "StudentsCourses");
        }
    }
}
