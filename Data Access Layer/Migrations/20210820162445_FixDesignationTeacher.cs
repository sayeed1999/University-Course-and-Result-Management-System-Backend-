using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class FixDesignationTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Designations_DesignationId1",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_DesignationId1",
                table: "Teachers");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_LengthOfDeptName",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DesignationId1",
                table: "Teachers");

            migrationBuilder.AlterColumn<byte>(
                name: "DesignationId",
                table: "Teachers",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DesignationId",
                table: "Teachers",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Designations_DesignationId",
                table: "Teachers",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Designations_DesignationId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_DesignationId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "DesignationId",
                table: "Teachers",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<byte>(
                name: "DesignationId1",
                table: "Teachers",
                type: "tinyint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DesignationId1",
                table: "Teachers",
                column: "DesignationId1");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_LengthOfDeptName",
                table: "Departments",
                sql: "len(name) >= 3");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Designations_DesignationId1",
                table: "Teachers",
                column: "DesignationId1",
                principalTable: "Designations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
