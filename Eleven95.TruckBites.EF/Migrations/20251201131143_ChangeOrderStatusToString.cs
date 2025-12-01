using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eleven95.TruckBites.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderStatusToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            
            migrationBuilder.Sql(@"
            UPDATE `truckbites`.Orders
            SET `Status` = CASE `Status`
                WHEN 0 THEN 'Created'
                WHEN 1 THEN 'PendingPayment'
                WHEN 2 THEN 'PaymentFailed'
                WHEN 3 THEN 'PaymentSucceeded'
                WHEN 4 THEN 'PendingConfirmation'
                WHEN 5 THEN 'Confirmed'
                WHEN 6 THEN 'Processing'
                WHEN 7 THEN 'Completed'
                WHEN 8 THEN 'Cancelled'
                WHEN 9 THEN 'Failed'
                ELSE 'Unknown'
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            UPDATE `truckbites`.Orders
            SET `Status` = CASE `Status`
                WHEN 'Created' THEN 0
                WHEN 'PendingPayment' THEN 1
                WHEN 'PaymentFailed' THEN 2
                WHEN 'PaymentSucceeded' THEN 3
                WHEN 'PendingConfirmation' THEN 4
                WHEN 'Confirmed' THEN 5
                WHEN 'Processing' THEN 6
                WHEN 'Completed' THEN 7
                WHEN 'Cancelled' THEN 8
                WHEN 'Failed' THEN 9
                ELSE 0
            END
        ");
            
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
