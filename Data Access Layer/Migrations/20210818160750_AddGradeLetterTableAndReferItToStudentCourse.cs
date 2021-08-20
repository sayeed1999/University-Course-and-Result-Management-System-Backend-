using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddGradeLetterTableAndReferItToStudentCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "StudentsCourses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GradeLetters",
                columns: table => new
                {
                    Grade = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLetters", x => x.Grade);
                });

            migrationBuilder.InsertData(
                table: "GradeLetters",
                column: "Grade",
                values: new object[]
                {
                    "A+",
                    "A",
                    "A-",
                    "B+",
                    "B",
                    "B-",
                    "C+",
                    "C",
                    "C-",
                    "D+",
                    "D",
                    "D-",
                    "F"
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_Grade",
                table: "StudentsCourses",
                column: "Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_GradeLetters_Grade",
                table: "StudentsCourses",
                column: "Grade",
                principalTable: "GradeLetters",
                principalColumn: "Grade",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_GradeLetters_Grade",
                table: "StudentsCourses");

            migrationBuilder.DropTable(
                name: "GradeLetters");

            migrationBuilder.DropIndex(
                name: "IX_StudentsCourses_Grade",
                table: "StudentsCourses");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentsCourses");
        }
    }
}
