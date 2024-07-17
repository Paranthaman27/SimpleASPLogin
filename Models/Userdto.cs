using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleASPLogin.Models
{
	public class Userdto
	{
		[Key]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}

