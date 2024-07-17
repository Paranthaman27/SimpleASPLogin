using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleASPLogin.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Vehicle_Type { get; set; }

        [Required]
        public int Vehicle_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan BookingTime { get; set; }

        [Required]
        public string? PickupLocation { get; set; }

        [Required]
        public string? DeliveryLocation { get; set; }

        [Required]
        public int RentalDuration { get; set; } 

        [Required]
        public double Distance { get; set; } 

        public double TotalPrice { get; set; }

        [ForeignKey("DriverEmail")]
        public string? DriverEmail { get; set; }
    }
}
