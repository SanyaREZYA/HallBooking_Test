using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HallBooking_Test.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHallOption_Bookings_BookingId",
                table: "BookingHallOption");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingHallOption_HallOptions_HallOptionId",
                table: "BookingHallOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingHallOption",
                table: "BookingHallOption");

            migrationBuilder.RenameTable(
                name: "BookingHallOption",
                newName: "BookingHallOptions");

            migrationBuilder.RenameIndex(
                name: "IX_BookingHallOption_HallOptionId",
                table: "BookingHallOptions",
                newName: "IX_BookingHallOptions_HallOptionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingHallOptions",
                table: "BookingHallOptions",
                columns: new[] { "BookingId", "HallOptionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHallOptions_Bookings_BookingId",
                table: "BookingHallOptions",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHallOptions_HallOptions_HallOptionId",
                table: "BookingHallOptions",
                column: "HallOptionId",
                principalTable: "HallOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHallOptions_Bookings_BookingId",
                table: "BookingHallOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingHallOptions_HallOptions_HallOptionId",
                table: "BookingHallOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingHallOptions",
                table: "BookingHallOptions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "BookingHallOptions",
                newName: "BookingHallOption");

            migrationBuilder.RenameIndex(
                name: "IX_BookingHallOptions_HallOptionId",
                table: "BookingHallOption",
                newName: "IX_BookingHallOption_HallOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingHallOption",
                table: "BookingHallOption",
                columns: new[] { "BookingId", "HallOptionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHallOption_Bookings_BookingId",
                table: "BookingHallOption",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHallOption_HallOptions_HallOptionId",
                table: "BookingHallOption",
                column: "HallOptionId",
                principalTable: "HallOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
