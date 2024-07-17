using System;
using SimpleASPLogin.Models;
using System.Collections.Generic;

namespace SimpleASPLogin.IService
{
	public interface INotiService
	{
		List<Noti> GetNotifications(string nToUserEmail, bool bIsGetOnlyUnRead);
        List<Noti> GetAllViewNotifications();
        void AddNotification(Noti notification);
        List<Noti> GetUnreadNotifications(string email);
        List<Noti> GetReadNotifications(string email);
        void MarkAsRead(int notificationId);

    }
}

