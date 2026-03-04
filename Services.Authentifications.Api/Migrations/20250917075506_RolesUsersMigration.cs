using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Authentifications.Api.Migrations
{
    /// <inheritdoc />
    public partial class RolesUsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesUsers",
                table: "RolesUsers");

            migrationBuilder.DropIndex(
                name: "IX_RolesUsers_UsersUserId",
                table: "RolesUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesUsers",
                table: "RolesUsers",
                columns: new[] { "UsersUserId", "RolesRoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_RolesRoleId",
                table: "RolesUsers",
                column: "RolesRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesUsers",
                table: "RolesUsers");

            migrationBuilder.DropIndex(
                name: "IX_RolesUsers_RolesRoleId",
                table: "RolesUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesUsers",
                table: "RolesUsers",
                columns: new[] { "RolesRoleId", "UsersUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_UsersUserId",
                table: "RolesUsers",
                column: "UsersUserId");
        }
    }
}
