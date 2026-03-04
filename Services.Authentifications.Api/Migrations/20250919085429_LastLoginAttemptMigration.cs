using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Authentifications.Api.Migrations
{
    /// <inheritdoc />
    public partial class LastLoginAttemptMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAttempt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PasswordTrys",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginAttempt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordTrys",
                table: "Users");
        }
    }
}
