using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleASPLogin.IService;
using SimpleASPLogin.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleASPLogin.Controllers
{
    public class NotificationsController : BaseController
    {
        private readonly INotiService _notiService;

        public NotificationsController(INotiService notiService)
        {
            _notiService = notiService;
        }

        public IActionResult Index()
        {
            var email = HttpContext.Session?.GetString("Email");
            var role = HttpContext.Session?.GetString("Role");
                //GetUserNotifications(email);
            var unreadNotifications = _notiService.GetUnreadNotifications(email);
            var readNotifications = _notiService.GetReadNotifications(email);

            var model = new NotificationViewModel
            {
                UnreadNotifications = unreadNotifications,
                ReadNotifications = readNotifications
            };

            return View(model);
        }

        public IActionResult SendMessage()
        {
            return View(new SendMessageViewModel());
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var notification = new Noti
                {
                    FromUserEmail = model.FromUserEmail,
                    ToUserEmail = model.ToUserEmail,
                    NotiHeader = model.NotiHeader,
                    NotiBody = model.NotiBody,
                    CreatedDate = model.CreatedDate,
                    IsRead = false
                };
                _notiService.AddNotification(notification);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            _notiService.MarkAsRead(id);
            return Json(new { success = true });
        }

        public IActionResult GetUserNotifications(string userEmail, bool onlyUnread = false)
        {
            var notifications = _notiService.GetNotifications(userEmail, onlyUnread);
            return View("Index", notifications);
        }
    }
}

