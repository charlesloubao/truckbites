using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eleven95.TruckBites.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdatedAtAndPaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Payments",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Payments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Payments");
        }
    }
}
