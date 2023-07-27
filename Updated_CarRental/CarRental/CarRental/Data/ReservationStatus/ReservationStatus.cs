using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.ReservationStatus
{
    [Table("ReservationStatus")]
    public class ReservationStatus
    {
        [Key]
        public byte ReservationId { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string Status { get; set; }

        //public virtual ICollection<Rental> Rentals { get; set; }

    }
}
