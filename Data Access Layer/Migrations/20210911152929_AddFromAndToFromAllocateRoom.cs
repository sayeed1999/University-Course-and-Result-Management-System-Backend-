using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddFromAndToFromAllocateRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "From",
                table: "AllocateClassrooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "To",
                table: "AllocateClassrooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "From",
                table: "AllocateClassroomHistories",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "To",
                table: "AllocateClassroomHistories",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "AllocateClassrooms");

            migrationBuilder.DropColumn(
                name: "To",
                table: "AllocateClassrooms");

            migrationBuilder.DropColumn(
                name: "From",
                table: "AllocateClassroomHistories");

            migrationBuilder.DropColumn(
                name: "To",
                table: "AllocateClassroomHistories");
        }
    }
}
