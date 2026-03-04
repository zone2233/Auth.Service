using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Authentifications.Api.Migrations
{
    /// <inheritdoc />
    public partial class TOTPKeyMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TOTPKey",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TOTPKey",
                table: "Users");
        }
    }
}
