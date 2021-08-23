using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixAllocateClassroomHistoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocateClassroomHistories_Courses_CourseCode1_CourseDepartmentId",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropTable(
                name: "CoursesHistory");

            migrationBuilder.DropTable(
                name: "StudentsCoursesHistories");

            migrationBuilder.DropIndex(
                name: "IX_AllocateClassroomHistories_CourseCode1_CourseDepartmentId",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropColumn(
                name: "CourseCode1",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropColumn(
                name: "CourseDepartmentId",
                table: "AllocateClassroomHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CourseCode",
                table: "AllocateClassroomHistories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_CourseCode",
                table: "AllocateClassroomHistories",
                column: "CourseCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocateClassroomHistories_Courses_CourseCode",
                table: "AllocateClassroomHistories",
                column: "CourseCode",
                principalTable: "Courses",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocateClassroomHistories_Courses_CourseCode",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropIndex(
                name: "IX_AllocateClassroomHistories_CourseCode",
                table: "AllocateClassroomHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CourseCode",
                table: "AllocateClassroomHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CourseCode1",
                table: "AllocateClassroomHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseDepartmentId",
                table: "AllocateClassroomHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoursesHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    NthHistory = table.Column<int>(type: "int", nullable: false),
                    SemisterId = table.Column<byte>(type: "tinyint", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesHistory_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesHistory_Semisters_SemisterId",
                        column: x => x.SemisterId,
                        principalTable: "Semisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesHistory_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentsCoursesHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseCode1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseDepartmentId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NthHistory = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCoursesHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsCoursesHistories_Courses_CourseCode1_CourseDepartmentId",
                        columns: x => new { x.CourseCode1, x.CourseDepartmentId },
                        principalTable: "Courses",
                        principalColumns: new[] { "Code", "DepartmentId" },
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_CourseCode1_CourseDepartmentId",
                table: "AllocateClassroomHistories",
                columns: new[] { "CourseCode1", "CourseDepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistory_DepartmentId",
                table: "CoursesHistory",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistory_SemisterId",
                table: "CoursesHistory",
                column: "SemisterId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistory_TeacherId",
                table: "CoursesHistory",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_CourseCode1_CourseDepartmentId",
                table: "StudentsCoursesHistories",
                columns: new[] { "CourseCode1", "CourseDepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_Grade",
                table: "StudentsCoursesHistories",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_StudentId",
                table: "StudentsCoursesHistories",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocateClassroomHistories_Courses_CourseCode1_CourseDepartmentId",
                table: "AllocateClassroomHistories",
                columns: new[] { "CourseCode1", "CourseDepartmentId" },
                principalTable: "Courses",
                principalColumns: new[] { "Code", "DepartmentId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
