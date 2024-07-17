//using System.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using SimpleASPLogin.Models;

//namespace SimpleASPLogin.Controllers;

//public class HomeController : Controller
//{
//    private readonly DbConnect dbConnect;
//    private readonly ILogger<HomeController> _logger;

//    public HomeController(ILogger<HomeController> logger)
//    {
//        _logger = logger;
//        dbConnect = new DbConnect();
//    }
//    public IActionResult Register()
//    {
//        return View();
//    }
//    public IActionResult Index()
//    {
//        return View();
//    }
//    public IActionResult Privacy()
//    {
//        return View();
//    }

//    public IActionResult DIndex()
//    {
//        var drivers = dbConnect.GetDrivers();
//        return View(drivers);
//    }

//    [HttpPost]
//    public ActionResult Register(User user)
//    {
//        if (ModelState.IsValid)
//        {
//            dbConnect.Register(user);

//            return RedirectToAction("Login");
//        }
//        else
//        {

//            return View(user);
//        }
//    }

//    public IActionResult Login()
//    {
//        return View();
//    }

//    [HttpPost]
//    public ActionResult Login(User user)
//    {
//        if (ModelState.IsValid)
//        {
//            if (dbConnect.Login(user)) {
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                ModelState.AddModelError("", "Invalid userid or password");
//                return View(user);
//            }

//        }
//        else
//        {
//            return View(user);
//        }
//    }
//    // GET: Driver/Create
//    public IActionResult Driver()
//    {
//        return View();
//    }

//    // POST: Driver/Create
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public ActionResult Driver(Driver driver)
//    {
//        if (ModelState.IsValid)
//        {
//            dbConnect.AddDriver(driver);
//            return RedirectToAction(nameof(DIndex));
//        }
//        return View(driver);
//    }

//    // GET: Driver/Edit/5
//    public IActionResult Edit(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var driver = dbConnect.GetDrivers().Find(d => d.Driver_id == id);
//        if (driver == null)
//        {
//            return NotFound();
//        }
//        return View(driver);
//    }

//    // POST: Driver/Edit/5
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Edit(int id, Driver driver)
//    {
//        if (id != driver.Driver_id)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {
//            dbConnect.UpdateDriver(driver);
//            return RedirectToAction(nameof(DIndex));
//        }
//        return View(driver);
//    }

//    // GET: Driver/Delete/5
//    public IActionResult Delete(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var driver = dbConnect.GetDrivers().Find(d => d.Driver_id == id);
//        if (driver == null)
//        {
//            return NotFound();
//        }

//        return View(driver);
//    }

//    // POST: Driver/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public IActionResult DeleteConfirmed(int id)
//    {
//        dbConnect.DeleteDriver(id);
//        return RedirectToAction(nameof(DIndex));
//    }
//    // GET: Driver/Create
//    public IActionResult VIndex()
//    {
//        var vehicles = dbConnect.GetVehicles();
//        return View(vehicles);
//    }

//    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//    public IActionResult Error()
//    {
//        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//    }
//}



using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleASPLogin.Filters;
using SimpleASPLogin.Models;

namespace SimpleASPLogin.Controllers
{
    
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[SessionAuthorize]
        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }

        public IActionResult Login()
       {
            return View();
        }

        public IActionResult AdminDashBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Invoice()
        {
            return View(Invoice);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
