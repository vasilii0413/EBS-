using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    [Table("Rental")]
    public class Rental
    {
        [Key]
        public short RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public short VehicleId { get; set; }
        public byte ReservationId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser  User { get; set; }

        [ForeignKey("ReservationId")]
        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}
