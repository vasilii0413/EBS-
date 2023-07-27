using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Transmissions
{
    [Table("Transmission")]
    public class Transmission
    {
        [Key]
        public byte TransmissionId { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)")]
        public string TransmissionType { get; set; }

        //public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
