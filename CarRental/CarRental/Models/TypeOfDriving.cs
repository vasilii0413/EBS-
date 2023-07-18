using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    [Table("DriveType")]
    public class TypeOfDriving
    {
        [Key]
        public byte DriveTypeId { get; set; }
        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string TypeDrive { get; set; }
        
        //public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
