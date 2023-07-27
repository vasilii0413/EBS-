using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.VehicleTypes
{
    [Table("VehicleType")]
    public class VehicleType
    {
        [Key]
        public byte VehicleTypeId { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string VehicleTypeName { get; set; }

        //public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
