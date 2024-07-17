using System;
namespace SimpleASPLogin.Models
{
    public class Noti
    {
        public int NotiId { get; set; }
        public string? FromUserEmail { get; set; }
        public string? ToUserEmail { get; set; }
        public string NotiHeader { get; set; } = "";
        public string NotiBody { get; set; } = "";
        public bool IsRead { get; set; } = false;
        public string Url { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedDateSt => this.CreatedDate.ToString("dd-MMM-yyyy HH:mm:ss");
        public string IsReadSt => this.IsRead ? "YES" : "NO";

        //public string FromUserName { get; set; } = "";
        //public string ToUserName { get; set; } = "";
    }
}

