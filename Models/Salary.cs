using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleASPLogin.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [Required]
        public string DriverEmail { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
    }
}
