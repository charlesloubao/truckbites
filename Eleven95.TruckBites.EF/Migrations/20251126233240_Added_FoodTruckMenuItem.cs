using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Eleven95.TruckBites.EF.Migrations
{
    /// <inheritdoc />
    public partial class Added_FoodTruckMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodTruckMenuItems",
                columns: table => new
                {
                    FoodTruckMenuItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FoodTruckId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSoldOut = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTruckMenuItems", x => x.FoodTruckMenuItemId);
                    table.ForeignKey(
                        name: "FK_FoodTruckMenuItems_FoodTrucks_FoodTruckId",
                        column: x => x.FoodTruckId,
                        principalTable: "FoodTrucks",
                        principalColumn: "FoodTruckId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTruckMenuItems_FoodTruckId",
                table: "FoodTruckMenuItems",
                column: "FoodTruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodTruckMenuItems");
        }
    }
}
