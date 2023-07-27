using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Fuel
{
    [Table("Fuel")]
    public class Fuel
    {
        [Key]
        public byte FuelId { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)")]
        public string FuelType { get; set; }
       
        //public virtual ICollection<Vehicle> Vehicles { get; set; }


    }
}
