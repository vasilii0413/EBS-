using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class Fuel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Fuel_TempId1",
                table: "Fuel");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "Fuel",
                newName: "FuelId");

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Fuel",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fuel",
                table: "Fuel",
                column: "FuelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fuel",
                table: "Fuel");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Fuel");

            migrationBuilder.RenameColumn(
                name: "FuelId",
                table: "Fuel",
                newName: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Fuel_TempId1",
                table: "Fuel",
                column: "TempId1");
        }
    }
}
