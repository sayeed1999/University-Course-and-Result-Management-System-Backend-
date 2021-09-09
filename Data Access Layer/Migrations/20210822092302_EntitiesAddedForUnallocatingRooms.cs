using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class EntitiesAddedForUnallocatingRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AllocateClassroomHistories",
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
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnallocatingRoomsCountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocateClassroomHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocateClassroomHistories_Courses_CourseCode1_CourseDepartmentId",
                        columns: x => new { x.CourseCode1, x.CourseDepartmentId },
                        principalTable: "Courses",
                        principalColumns: new[] { "Code", "DepartmentId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllocateClassroomHistories_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateClassroomHistories_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateClassroomHistories_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateClassroomHistories_UnallocatingRoomsCounts_UnallocatingRoomsCountId",
                        column: x => x.UnallocatingRoomsCountId,
                        principalTable: "UnallocatingRoomsCounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_CourseCode1_CourseDepartmentId",
                table: "AllocateClassroomHistories",
                columns: new[] { "CourseCode1", "CourseDepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_DayId",
                table: "AllocateClassroomHistories",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_DepartmentId",
                table: "AllocateClassroomHistories",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_RoomId",
                table: "AllocateClassroomHistories",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateClassroomHistories_UnallocatingRoomsCountId",
                table: "AllocateClassroomHistories",
                column: "UnallocatingRoomsCountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocateClassroomHistories");

            migrationBuilder.DropTable(
                name: "UnallocatingRoomsCounts");
        }
    }
}
