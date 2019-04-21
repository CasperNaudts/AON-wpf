using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperChat.Data.Migrations
{
    public partial class updates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Users_UserId",
                table: "Keys");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Keys",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Users_UserId",
                table: "Keys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Users_UserId",
                table: "Keys");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Keys",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Users_UserId",
                table: "Keys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
