using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddStudentsCoursesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsCourses",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCourses", x => new { x.DepartmentId, x.CourseCode, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Courses_CourseCode_DepartmentId",
                        columns: x => new { x.CourseCode, x.DepartmentId },
                        principalTable: "Courses",
                        principalColumns: new[] { "Code", "DepartmentId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_CourseCode_DepartmentId",
                table: "StudentsCourses",
                columns: new[] { "CourseCode", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_StudentId",
                table: "StudentsCourses",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsCourses");
        }
    }
}
