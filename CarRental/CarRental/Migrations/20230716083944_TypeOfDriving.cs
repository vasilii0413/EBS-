using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class TypeOfDriving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TypeOfDriving_DriveTypeId",
                table: "Vehicle");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TypeOfDriving_TempId1",
                table: "TypeOfDriving");

            migrationBuilder.RenameTable(
                name: "TypeOfDriving",
                newName: "DriveType");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "DriveType",
                newName: "DriveTypeId");

            migrationBuilder.AddColumn<string>(
                name: "TypeDrive",
                table: "DriveType",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveType",
                table: "DriveType",
                column: "DriveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_DriveType_DriveTypeId",
                table: "Vehicle",
                column: "DriveTypeId",
                principalTable: "DriveType",
                principalColumn: "DriveTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_DriveType_DriveTypeId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveType",
                table: "DriveType");

            migrationBuilder.DropColumn(
                name: "TypeDrive",
                table: "DriveType");

            migrationBuilder.RenameTable(
                name: "DriveType",
                newName: "TypeOfDriving");

            migrationBuilder.RenameColumn(
                name: "DriveTypeId",
                table: "TypeOfDriving",
                newName: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TypeOfDriving_TempId1",
                table: "TypeOfDriving",
                column: "TempId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TypeOfDriving_DriveTypeId",
                table: "Vehicle",
                column: "DriveTypeId",
                principalTable: "TypeOfDriving",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
