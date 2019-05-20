using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RineServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RineUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RineUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserRequestId = table.Column<int>(nullable: false),
                    UserRecvId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Actioned = table.Column<DateTime>(nullable: true),
                    Accepted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendship_RineUser_UserRecvId",
                        column: x => x.UserRecvId,
                        principalTable: "RineUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendship_RineUser_UserRequestId",
                        column: x => x.UserRequestId,
                        principalTable: "RineUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RineMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    Sent = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    SenderId = table.Column<int>(nullable: true),
                    ReceiverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RineMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RineMessage_RineUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "RineUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RineMessage_RineUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "RineUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_UserRecvId",
                table: "Friendship",
                column: "UserRecvId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_UserRequestId",
                table: "Friendship",
                column: "UserRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RineMessage_ReceiverId",
                table: "RineMessage",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_RineMessage_SenderId",
                table: "RineMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_RineUser_Username",
                table: "RineUser",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "RineMessage");

            migrationBuilder.DropTable(
                name: "RineUser");
        }
    }
}
