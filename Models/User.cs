using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleASPLogin.Models
{
	public class User
	{
		[Required (ErrorMessage ="User  Name is Required")]
		public string? Username { get; set; }

        [Key]
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set;}

        [Required (ErrorMessage = "password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}

