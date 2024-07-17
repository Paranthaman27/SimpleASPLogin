using System;
using System.ComponentModel.DataAnnotations;
namespace SimpleASPLogin.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Key]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        public string Role { get; set; }

        public string D_Name { get; set; }
        
        public string DAddress { get; set; }
        public string D_Pno { get; set; }
    }
}
