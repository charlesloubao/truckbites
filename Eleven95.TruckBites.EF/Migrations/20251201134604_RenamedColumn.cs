using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eleven95.TruckBites.EF.Migrations
{
    /// <inheritdoc />
    public partial class RenamedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentProcessorType",
                table: "Payments",
                newName: "PaymentProcessor");

            migrationBuilder.AddColumn<string>(
                name: "PaymentProcessor",
                table: "Payouts",
                type: "text",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentProcessor",
                table: "Payouts");

            migrationBuilder.RenameColumn(
                name: "PaymentProcessor",
                table: "Payments",
                newName: "PaymentProcessorType");
        }
    }
}
