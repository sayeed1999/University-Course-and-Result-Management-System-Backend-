using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddTeachersAndDesignationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semister_SemisterId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semister",
                table: "Semister");

            migrationBuilder.RenameTable(
                name: "Semister",
                newName: "Semisters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semisters",
                table: "Semisters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contact = table.Column<long>(type: "bigint", nullable: false),
                    DesignationId1 = table.Column<byte>(type: "tinyint", nullable: true),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreditToBeTaken = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.CheckConstraint("CHK_TeacherEmailInCorrectFormat", "Email like '%_@_%._%'");
                    table.CheckConstraint("CHK_TeacherContactInCorrectFormat", "LEN(CAST(Contact as varchar(max))) between 6 and 15");
                    table.CheckConstraint("CHK_CreditToBeTakenByTeacher", "CreditToBeTaken !< 0");
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Designations_DesignationId1",
                        column: x => x.DesignationId1,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Lecturer" },
                    { (byte)2, "Assistant Lecturer" },
                    { (byte)3, "Professor" },
                    { (byte)4, "Associate Professor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designations_Name",
                table: "Designations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DesignationId1",
                table: "Teachers",
                column: "DesignationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Email",
                table: "Teachers",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semisters_SemisterId",
                table: "Courses",
                column: "SemisterId",
                principalTable: "Semisters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semisters_SemisterId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semisters",
                table: "Semisters");

            migrationBuilder.RenameTable(
                name: "Semisters",
                newName: "Semister");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semister",
                table: "Semister",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semister_SemisterId",
                table: "Courses",
                column: "SemisterId",
                principalTable: "Semister",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
