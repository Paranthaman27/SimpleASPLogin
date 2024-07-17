using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleASPLogin.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public int? CompanyId { get; set; } // Nullable if IsGST is false

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int StartKm { get; set; }

        [Required]
        public int EndKm { get; set; }

        [Required]
        public decimal DieselCost { get; set; }

        public bool IsGST { get; set; }

    }
}
