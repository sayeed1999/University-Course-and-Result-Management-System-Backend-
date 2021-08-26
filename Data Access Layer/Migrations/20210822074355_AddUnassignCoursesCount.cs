using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddUnassignCoursesCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnassignCoursesCountId",
                table: "StudentsCoursesHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnassignCoursesCountId",
                table: "CoursesHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UnassignCoursesCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnassignCoursesCounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_UnassignCoursesCountId",
                table: "StudentsCoursesHistories",
                column: "UnassignCoursesCountId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistory_UnassignCoursesCountId",
                table: "CoursesHistory",
                column: "UnassignCoursesCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesHistory_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "CoursesHistory",
                column: "UnassignCoursesCountId",
                principalTable: "UnassignCoursesCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCoursesHistories_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "StudentsCoursesHistories",
                column: "UnassignCoursesCountId",
                principalTable: "UnassignCoursesCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesHistory_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "CoursesHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCoursesHistories_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropTable(
                name: "UnassignCoursesCounts");

            migrationBuilder.DropIndex(
                name: "IX_StudentsCoursesHistories_UnassignCoursesCountId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropIndex(
                name: "IX_CoursesHistory_UnassignCoursesCountId",
                table: "CoursesHistory");

            migrationBuilder.DropColumn(
                name: "UnassignCoursesCountId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "UnassignCoursesCountId",
                table: "CoursesHistory");
        }
    }
}
