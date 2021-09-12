using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixStudentCourseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseHistories_DepartmentId",
                table: "StudentCourseHistories",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseHistories_Departments_DepartmentId",
                table: "StudentCourseHistories",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseHistories_Departments_DepartmentId",
                table: "StudentCourseHistories");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourseHistories_DepartmentId",
                table: "StudentCourseHistories");
        }
    }
}
