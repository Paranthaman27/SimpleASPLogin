using System;
namespace SimpleASPLogin.Models
{
	public class SendMessageViewModel
	{
        public string? FromUserEmail { get; set; }
        public string? ToUserEmail { get; set; }
        public string NotiHeader { get; set; } = "";
        public string NotiBody { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}