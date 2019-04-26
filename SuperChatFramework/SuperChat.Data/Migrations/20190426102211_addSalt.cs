using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperChat.Data.Migrations
{
    public partial class addSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IV",
                table: "Messages",
                newName: "Iv");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Iv",
                table: "Messages",
                newName: "IV");
        }
    }
}
