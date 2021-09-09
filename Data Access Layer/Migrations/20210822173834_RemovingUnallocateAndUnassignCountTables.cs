using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class RemovingUnallocateAndUnassignCountTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocateClassroomHistories_UnallocatingRoomsCounts_UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesHistory_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "CoursesHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCoursesHistories_Courses_CourseCode_DepartmentId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCoursesHistories_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropTable(
                name: "UnallocatingRoomsCounts");

            migrationBuilder.DropTable(
                name: "UnassignCoursesCounts");

            migrationBuilder.DropIndex(
                name: "IX_StudentsCoursesHistories_CourseCode_DepartmentId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropIndex(
                name: "IX_StudentsCoursesHistories_UnassignCoursesCountId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropIndex(
                name: "IX_CoursesHistory_UnassignCoursesCountId",
                table: "CoursesHistory");

            migrationBuilder.DropIndex(
                name: "IX_AllocateClassroomHistories_UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropColumn(
                name: "UnassignCoursesCountId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "UnassignCoursesCountId",
                table: "CoursesHistory");

            migrationBuilder.DropColumn(
                name: "UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CourseCode",
                table: "StudentsCoursesHistories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");


            migrationBuilder.AddColumn<int>(
                name: "CourseDepartmentId",
                table: "StudentsCoursesHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_CourseCode_CourseDepartmentId",
                table: "StudentsCoursesHistories",
                columns: new[] { "CourseCode", "CourseDepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCoursesHistories_Courses_CourseCode_CourseDepartmentId",
                table: "StudentsCoursesHistories",
                columns: new[] { "CourseCode", "CourseDepartmentId" },
                principalTable: "Courses",
                principalColumns: new[] { "Code", "DepartmentId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCoursesHistories_Courses_CourseCode1_CourseDepartmentId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropIndex(
                name: "IX_StudentsCoursesHistories_CourseCode1_CourseDepartmentId",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "CourseCode1",
                table: "StudentsCoursesHistories");

            migrationBuilder.DropColumn(
                name: "CourseDepartmentId",
                table: "StudentsCoursesHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CourseCode",
                table: "StudentsCoursesHistories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddColumn<int>(
                name: "UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UnallocatingRoomsCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnallocatingRoomsCounts", x => x.Id);
                });

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
                name: "IX_StudentsCoursesHistories_CourseCode_DepartmentId",
                table: "StudentsCoursesHistories",
                columns: new[] { "CourseCode", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCoursesHistories_UnassignCoursesCountId",
                table: "StudentsCoursesHistories",
                column: "UnassignCoursesCountId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesHistory_UnassignCoursesCountId",
                table: "CoursesHistory",
                column: "UnassignCoursesCountId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories",
                column: "UnallocatingRoomsCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocateClassroomHistories_UnallocatingRoomsCounts_UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories",
                column: "UnallocatingRoomsCountId",
                principalTable: "UnallocatingRoomsCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesHistory_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "CoursesHistory",
                column: "UnassignCoursesCountId",
                principalTable: "UnassignCoursesCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCoursesHistories_Courses_CourseCode_DepartmentId",
                table: "StudentsCoursesHistories",
                columns: new[] { "CourseCode", "DepartmentId" },
                principalTable: "Courses",
                principalColumns: new[] { "Code", "DepartmentId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCoursesHistories_UnassignCoursesCounts_UnassignCoursesCountId",
                table: "StudentsCoursesHistories",
                column: "UnassignCoursesCountId",
                principalTable: "UnassignCoursesCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
