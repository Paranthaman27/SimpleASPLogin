using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleASPLogin.Models
{
	public class Vehicle
	{
        [Key]
        [Required(ErrorMessage = "Vehicle Id is Required")]
        public int? Vehicle_id { get; set; }

        [Required(ErrorMessage = "Vehicle Name is Required")]
        [StringLength(50, ErrorMessage = "Vehicle Name cannot be longer than 50 characters")]
        public string? Vehicle_Name { get; set; }

        [Required(ErrorMessage = "Vehicle Register Number is Required")]
        [StringLength(20, ErrorMessage = "Vehicle Register Number cannot be longer than 20 characters")]
        public string? Vehicle_Reg { get; set; }

        [Required(ErrorMessage = "Vehicle Capacity in Tons")]
        public string Vehicle_Type { get; set; }

        [ForeignKey("DriverEmail")]
        public string? DriverEmail { get; set; }

    }
}

