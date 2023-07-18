using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class Transmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Transmission_TempId1",
                table: "Transmission");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "Transmission",
                newName: "TransmissionId");

            migrationBuilder.AddColumn<string>(
                name: "TransmissionType",
                table: "Transmission",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transmission",
                table: "Transmission",
                column: "TransmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transmission",
                table: "Transmission");

            migrationBuilder.DropColumn(
                name: "TransmissionType",
                table: "Transmission");

            migrationBuilder.RenameColumn(
                name: "TransmissionId",
                table: "Transmission",
                newName: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Transmission_TempId1",
                table: "Transmission",
                column: "TempId1");
        }
    }
}
