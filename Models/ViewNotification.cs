using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleASPLogin.Models
{
    public class ViewNotification
    {
        [Key]
        public int NotiId { get; set; }
        public string FromUserEmail { get; set; }
        public string ToUserEmail { get; set; }
        public string NotiHeader { get; set; }
        public string NotiBody { get; set; }
        public bool IsRead { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
    }
}
