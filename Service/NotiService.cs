using System;
using Microsoft.EntityFrameworkCore;
using SimpleASPLogin.Controllers;
using SimpleASPLogin.IService;
using SimpleASPLogin.Models;

namespace SimpleASPLogin.Service
{
    public class NotiService : INotiService
    {
        private readonly SimpleDbContext _dbContext;

        public NotiService(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddNotification(Noti notification)
        {
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
        }


        public List<Noti> GetUnreadNotifications(string nToUserEmail)
        {
            return _dbContext.Notifications
                .Where(n => n.ToUserEmail == nToUserEmail && !n.IsRead)
                .ToList();
        }

        public List<Noti> GetReadNotifications(string nToUserEmail)
        {
            return _dbContext.Notifications
                .Where(n => n.ToUserEmail == nToUserEmail && n.IsRead)
                .ToList();
        }

        public List<Noti> GetAllViewNotifications()
        {
            return _dbContext.Notifications.ToList();
        }

        public void MarkAsRead(int notificationId)
        {
            var notification = _dbContext.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                _dbContext.SaveChanges();
            }
        }

        public List<Noti> GetNotifications(string nToUserEmail, bool bIsGetOnlyUnRead)
        {
            var notifications = _dbContext.Notifications
                .Where(n => n.ToUserEmail == nToUserEmail && !n.IsRead)
                .ToList();

            return notifications.ToList();
        }
    }
}

