using Microsoft.EntityFrameworkCore.Migrations;

namespace RineServer.Migrations
{
    public partial class Received_To_Message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Received",
                table: "RineMessage",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Received",
                table: "RineMessage");
        }
    }
}
