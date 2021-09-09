using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixStudentCourseHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCourseHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NthHistory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourseHistories_Courses_CourseCode_DepartmentId",
                        columns: x => new { x.CourseCode, x.DepartmentId },
                        principalTable: "Courses",
                        principalColumns: new[] { "Code", "DepartmentId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourseHistories_GradeLetters_Grade",
                        column: x => x.Grade,
                        principalTable: "GradeLetters",
                        principalColumn: "Grade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourseHistories_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseHistories_CourseCode_DepartmentId",
                table: "StudentCourseHistories",
                columns: new[] { "CourseCode", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseHistories_Grade",
                table: "StudentCourseHistories",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseHistories_StudentId",
                table: "StudentCourseHistories",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourseHistories");
        }
    }
}
