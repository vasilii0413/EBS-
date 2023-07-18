using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class VehicleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_VehicleType_TempId1",
                table: "VehicleType");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "VehicleType",
                newName: "VehicleTypeId");

            migrationBuilder.AddColumn<string>(
                name: "VehicleTypeName",
                table: "VehicleType",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType",
                column: "VehicleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "VehicleTypeName",
                table: "VehicleType");

            migrationBuilder.RenameColumn(
                name: "VehicleTypeId",
                table: "VehicleType",
                newName: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_VehicleType_TempId1",
                table: "VehicleType",
                column: "TempId1");
        }
    }
}
