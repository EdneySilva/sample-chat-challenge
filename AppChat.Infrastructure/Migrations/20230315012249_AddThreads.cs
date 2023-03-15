using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThreads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageDelivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MessageRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChatRoomId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Threads_Rooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ChatRoomId",
                table: "Threads",
                column: "ChatRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Threads");
        }
    }
}
