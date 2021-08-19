using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AgainAddingAllocateClassrooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllocateClassrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CourseCode1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseDepartmentId = table.Column<int>(type: "int", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocateClassrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocateClassrooms_Courses_CourseCode1_CourseDepartmentId",
                        columns: x => new { x.CourseCode1, x.CourseDepartmentId },
                        principalTable: "Courses",
                        principalColumns: new[] { "Code", "DepartmentId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllocateClassrooms_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateClassrooms_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateClassrooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassrooms_CourseCode1_CourseDepartmentId",
                table: "AllocateClassrooms",
                columns: new[] { "CourseCode1", "CourseDepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassrooms_DayId",
                table: "AllocateClassrooms",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassrooms_DepartmentId",
                table: "AllocateClassrooms",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassrooms_RoomId",
                table: "AllocateClassrooms",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocateClassrooms");
        }
    }
}
