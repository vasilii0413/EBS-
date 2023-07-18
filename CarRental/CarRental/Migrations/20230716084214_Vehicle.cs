using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class Vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Vehicle_TempId1",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "Vehicle",
                newName: "VehicleId");

            migrationBuilder.AlterColumn<byte>(
                name: "VehicleTypeId",
                table: "Vehicle",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "TransmissionId",
                table: "Vehicle",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "FuelId",
                table: "Vehicle",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "DriveTypeId",
                table: "Vehicle",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "BodyTypeId",
                table: "Vehicle",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "VehicleId",
                table: "Vehicle",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyPrice",
                table: "Vehicle",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vehicle",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Seats",
                table: "Vehicle",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_BodyTypeId",
                table: "Vehicle",
                column: "BodyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_DriveTypeId",
                table: "Vehicle",
                column: "DriveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_FuelId",
                table: "Vehicle",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_TransmissionId",
                table: "Vehicle",
                column: "TransmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleTypeId",
                table: "Vehicle",
                column: "VehicleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_BodyTypeId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_DriveTypeId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_FuelId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_TransmissionId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_VehicleTypeId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "DailyPrice",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Vehicle",
                newName: "TempId1");

            migrationBuilder.AlterColumn<byte>(
                name: "VehicleTypeId",
                table: "Vehicle",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<byte>(
                name: "TransmissionId",
                table: "Vehicle",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<byte>(
                name: "FuelId",
                table: "Vehicle",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<byte>(
                name: "DriveTypeId",
                table: "Vehicle",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<byte>(
                name: "BodyTypeId",
                table: "Vehicle",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<short>(
                name: "TempId1",
                table: "Vehicle",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Vehicle_TempId1",
                table: "Vehicle",
                column: "TempId1");
        }
    }
}
