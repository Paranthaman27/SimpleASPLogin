using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleASPLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using SimpleASPLogin.Filters;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace SimpleASPLogin.Controllers
{
    
    public class UserController : BaseController
    {
        private readonly SimpleDbContext _dbContext;
        private readonly ILogger<UserController> _logger;
       
        public UserController(SimpleDbContext dbContext, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.login.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View(user);
            }
            
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            ViewBag.Role = HttpContext.Session?.GetString("Role");
            return View();
        }

        //[HttpPost("Login")]
        //public IActionResult Login(Userdto userdto)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var existingUser = _dbContext.login.FirstOrDefault(u => u.Email == userdto.Email);
        //        var driver = _dbContext.Drivers.SingleOrDefault(d => d.DriverEmail == userdto.Email);
        //        if (existingUser != null)
        //        {
        //            if (existingUser.Password == userdto.Password)
        //            {
        //                HttpContext.Session.SetString("Email", existingUser.Email.ToString());
        //                HttpContext.Session.SetString("Role", existingUser.Role.ToString());
        //                var role= HttpContext.Session?.GetString("Role");
        //                ViewBag.Role = HttpContext.Session?.GetString("Role");
        //                ViewBag.Email = HttpContext.Session?.GetString("Email");
        //                if (role.Equals("Admin"))
        //                {
        //                    TempData["AlertMessage"] = "LoggedIn Successfully";
        //                    return RedirectToAction("AdminDashBoard","Home");
        //                }
        //                return RedirectToAction("Index", "Home");

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Invalid password");
        //                return View(userdto);
        //            }
        //        }
        //        else if(driver != null)
        //        {
        //            if (driver.Password == userdto.Password)
        //            {
        //                HttpContext.Session.SetString("Email", driver.DriverEmail.ToString());
        //                ViewBag.Role = HttpContext.Session?.GetString("Role");
        //                HttpContext.Session.SetString("Role", driver.Role.ToString());
        //                ViewBag.Email = HttpContext.Session?.GetString("Email");
        //                return RedirectToAction("DriverDashBoard", "Driver");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Invalid password");
        //                return View(userdto);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "User not found");
        //            return View(userdto);
        //        }
        //    }
        //    else
        //    {
        //        return View(userdto);
        //    }
        //}
        [HttpPost("Login")]
        public IActionResult Login(Userdto userdto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _dbContext.login.FirstOrDefault(u => u.Email == userdto.Email);
                var driver = _dbContext.Drivers.SingleOrDefault(d => d.DriverEmail == userdto.Email);
                if (existingUser != null)
                {
                    if (existingUser.Password == userdto.Password)
                    {
                        HttpContext.Session.SetString("Email", existingUser.Email.ToString());
                        HttpContext.Session.SetString("Role", existingUser.Role.ToString());
                        var role = HttpContext.Session?.GetString("Role");
                        ViewBag.Role = HttpContext.Session?.GetString("Role");
                        ViewBag.Email = HttpContext.Session?.GetString("Email");

                        if (HttpContext.Request.IsAjaxRequest())
                        {
                            return Json(new { success = true, role = role, redirectUrl = Url.Action("AdminDashBoard", "Home") });
                        }

                        if (role.Equals("Admin"))
                        {
                            TempData["AlertMessage"] = "LoggedIn Successfully";
                            return RedirectToAction("AdminDashBoard", "Home");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (HttpContext.Request.IsAjaxRequest())
                        {
                            return Json(new { success = false, message = "Invalid password" });
                        }
                        ModelState.AddModelError("", "Invalid password");
                        return View(userdto);
                    }
                }
                else if (driver != null)
                {
                    if (driver.Password == userdto.Password)
                    {
                        HttpContext.Session.SetString("Email", driver.DriverEmail.ToString());
                        ViewBag.Role = HttpContext.Session?.GetString("Role");
                        HttpContext.Session.SetString("Role", driver.Role.ToString());
                        ViewBag.Email = HttpContext.Session?.GetString("Email");

                        if (HttpContext.Request.IsAjaxRequest())
                        {
                            return Json(new { success = true, role = driver.Role, redirectUrl = Url.Action("DriverDashBoard", "Driver") });
                        }

                        return RedirectToAction("DriverDashBoard", "Driver");
                    }
                    else
                    {
                        if (HttpContext.Request.IsAjaxRequest())
                        {
                            return Json(new { success = false, message = "Invalid password" });
                        }
                        ModelState.AddModelError("", "Invalid password");
                        return View(userdto);
                    }
                }
                else
                {
                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        return Json(new { success = false, message = "User not found" });
                    }
                    ModelState.AddModelError("", "User not found");
                    return View(userdto);
                }
            }
            else
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
                return View(userdto);
            }
        }


        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.Role = null;
            return RedirectToAction("Login");
        }
    }
}
