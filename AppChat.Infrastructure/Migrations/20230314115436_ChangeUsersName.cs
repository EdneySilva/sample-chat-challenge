using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUsersName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersByRoom_Users_UsersUserName",
                table: "UsersByRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersByRoom_Accounts_UsersUserName",
                table: "UsersByRoom",
                column: "UsersUserName",
                principalTable: "Accounts",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersByRoom_Accounts_UsersUserName",
                table: "UsersByRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersByRoom_Users_UsersUserName",
                table: "UsersByRoom",
                column: "UsersUserName",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
