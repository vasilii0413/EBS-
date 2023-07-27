using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.BodyType
{
    [Table("BodyType")]
    public class BodyType
    {
        [Key]
        public byte BodyTypeId { get; set; }
        [StringLength(30), Column(TypeName = "varchar(30)")]
        public string Body { get; set; }
        //public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
