using CarRental.Data.Transmissions;
using CarRental.Data.TypeOfDrivings;
using CarRental.Data.VehicleTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Vehicles
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        public short VehicleId { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Column(TypeName = "tinyint")]
        public byte VehicleTypeId { get; set; }

        [Column(TypeName = "tinyint")]
        public byte BodyTypeId { get; set; }

        [Column(TypeName = "tinyint")]
        public byte TransmissionId { get; set; }

        [Column(TypeName = "tinyint")]
        public byte Seats { get; set; }

        [Column(TypeName = "tinyint")]
        public byte FuelId { get; set; }

        [Column(TypeName = "tinyint")]
        public byte DriveTypeId { get; set; }

        [Column(TypeName = "int")]
        public int Year { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal DailyPrice { get; set; }

        public string Image { get; set; }


        [ForeignKey("VehicleTypeId")]
        public virtual VehicleType VehicleType { get; set; }

        [ForeignKey("BodyTypeId")]
        public virtual BodyType.BodyType BodyType { get; set; }

        [ForeignKey("TransmissionId")]
        public virtual Transmission Transmission { get; set; }

        [ForeignKey("FuelId")]
        public virtual Fuel.Fuel Fuel { get; set; }

        [ForeignKey("DriveTypeId")]
        public virtual TypeOfDriving TypeOfDriving { get; set; }

        //public virtual ICollection<Rental> Rentals { get; set; }
    }
}
