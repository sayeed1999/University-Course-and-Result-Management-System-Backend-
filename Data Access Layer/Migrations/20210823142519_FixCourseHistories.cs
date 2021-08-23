using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixCourseHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursesHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    SemisterId = table.Column<byte>(type: "tinyint", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    NthHistory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesHistories_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesHistories_Semisters_SemisterId",
                        column: x => x.SemisterId,
                        principalTable: "Semisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesHistories_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistories_Code_DepartmentId",
                table: "CoursesHistories",
                columns: new[] { "Code", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistories_DepartmentId",
                table: "CoursesHistories",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistories_SemisterId",
                table: "CoursesHistories",
                column: "SemisterId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistories_TeacherId",
                table: "CoursesHistories",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesHistories");
        }
    }
}
