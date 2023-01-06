using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFriend_UserId",
                schema: "dbo",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_UserChat_UserId",
                schema: "dbo",
                table: "UserChat");

            migrationBuilder.DropIndex(
                name: "IX_GroupUser_UserId",
                schema: "dbo",
                table: "GroupUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_UserId_PhoneNumber",
                schema: "dbo",
                table: "UserFriend",
                columns: new[] { "UserId", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChat_UserId_ChatId",
                schema: "dbo",
                table: "UserChat",
                columns: new[] { "UserId", "ChatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                schema: "dbo",
                table: "User",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserId_GroupId",
                schema: "dbo",
                table: "GroupUser",
                columns: new[] { "UserId", "GroupId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFriend_UserId_PhoneNumber",
                schema: "dbo",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_UserChat_UserId_ChatId",
                schema: "dbo",
                table: "UserChat");

            migrationBuilder.DropIndex(
                name: "IX_User_PhoneNumber",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_GroupUser_UserId_GroupId",
                schema: "dbo",
                table: "GroupUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_UserId",
                schema: "dbo",
                table: "UserFriend",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChat_UserId",
                schema: "dbo",
                table: "UserChat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserId",
                schema: "dbo",
                table: "GroupUser",
                column: "UserId");
        }
    }
}
