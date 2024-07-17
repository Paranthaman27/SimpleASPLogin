using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleASPLogin.Models
{
    public class Driver
    {
        [Key]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? DriverEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Driver Name is required")]
        public string? D_Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? DAddress { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? D_Pno { get; set; }

        public string? Role { get; set; }
        
    }



}