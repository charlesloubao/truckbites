using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eleven95.TruckBites.EF.Migrations
{
    /// <inheritdoc />
    public partial class FoodTrucks_Removed_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FoodTrucks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FoodTrucks",
                type: "longtext",
                nullable: false);
        }
    }
}
