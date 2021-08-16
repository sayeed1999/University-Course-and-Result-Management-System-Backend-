using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddCoursesAndSemistersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Semister",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semister", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credit = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemisterId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => new { x.Code, x.DepartmentId });
                    table.CheckConstraint("CHK_LengthOfCodeOfCourse", "LEN(Code) >= 5");
                    table.CheckConstraint("CHK_CreditRangeOfCourse", "Credit BETWEEN 0.5 AND 5.0");
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Semister_SemisterId",
                        column: x => x.SemisterId,
                        principalTable: "Semister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Semister",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "1st" },
                    { (byte)2, "2nd" },
                    { (byte)3, "3rd" },
                    { (byte)4, "4th" },
                    { (byte)5, "5th" },
                    { (byte)6, "6th" },
                    { (byte)7, "7th" },
                    { (byte)8, "8th" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SemisterId",
                table: "Courses",
                column: "SemisterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Semister");
        }
    }
}
