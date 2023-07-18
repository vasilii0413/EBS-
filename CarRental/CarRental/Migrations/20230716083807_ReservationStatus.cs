using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class ReservationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ReservationStatus_TempId1",
                table: "ReservationStatus");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "ReservationStatus",
                newName: "ReservationId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ReservationStatus",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationStatus",
                table: "ReservationStatus",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationStatus",
                table: "ReservationStatus");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReservationStatus");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "ReservationStatus",
                newName: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ReservationStatus_TempId1",
                table: "ReservationStatus",
                column: "TempId1");
        }
    }
}
