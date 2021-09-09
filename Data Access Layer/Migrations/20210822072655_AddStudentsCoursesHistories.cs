using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddStudentsCoursesHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Departments_DepartmentId",
                table: "StudentsCourses");

            migrationBuilder.CreateTable(
                name: "StudentsCoursesHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCoursesHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsCoursesHistories_Courses_CourseCode_DepartmentId",
                        columns: x => new { x.CourseCode, x.DepartmentId },
                        principalTable: "Courses",
                        principalColumns: new[] { "Code", "DepartmentId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCoursesHistories_GradeLetters_Grade",
                        column: x => x.Grade,
                        principalTable: "GradeLetters",
                        principalColumn: "Grade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentsCoursesHistories_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_CourseCode_DepartmentId",
                table: "StudentsCoursesHistories",
                columns: new[] { "CourseCode", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_Grade",
                table: "StudentsCoursesHistories",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_StudentId",
                table: "StudentsCoursesHistories",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsCoursesHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Departments_DepartmentId",
                table: "StudentsCourses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
