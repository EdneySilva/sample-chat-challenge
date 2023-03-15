using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBotCommand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotCommands",
                columns: table => new
                {
                    Command = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotCommands", x => x.Command);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotCommands");
        }
    }
}
