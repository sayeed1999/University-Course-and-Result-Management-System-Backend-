using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class AddRoomsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                column: "Name",
                values: new object[]
                {
                    "A-101",
                    "A-102",
                    "A-103",
                    "A-104",
                    "B-101",
                    "B-102",
                    "B-103",
                    "B-104",
                    "C-101",
                    "C-102",
                    "C-103",
                    "C-104",
                    "D-101",
                    "D-102",
                    "D-103",
                    "D-104"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
