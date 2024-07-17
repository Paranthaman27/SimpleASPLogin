using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleASPLogin.Filters;
using SimpleASPLogin.Models;
using System.Linq;

namespace SimpleASPLogin.Controllers
{
    [SessionAuthorize]
    public class DriverController : BaseController
    {
        private readonly SimpleDbContext _dbContext;
        private readonly ILogger<DriverController> _logger;

        public DriverController(SimpleDbContext dbContext, ILogger<DriverController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult DIndex()
        {
            var drivers = _dbContext.Drivers.ToList();
            return View(drivers);
        }
        [RoleAuthorize("Admin")]
        public IActionResult Driver()
        {
            return View();
        }

        public IActionResult DriverDashBoard()
        {
            return View();
        }

        [HttpPost]
        [RoleAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Driver(Driver driver)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Drivers.Add(driver);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(DIndex));
            }
            return View(driver);
        }
        [RoleAuthorize("Driver")]
        public IActionResult Edit(string? Email)
        {
            if (Email == null)
            {
                return NotFound();
            }
            else
            {
                var driver = _dbContext.Drivers.Find(Email);
                if (driver == null)
                {
                    return View(driver);
                }
                return View(driver);
            }
            
        }

        [HttpPost]
        [RoleAuthorize("Driver")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Email, Driver driver)
        {
            if (Email == driver.DriverEmail)
            {
                _dbContext.Update(driver);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(DIndex));
            }

            else
            {
                return View(driver);
            }
            
        }
        [RoleAuthorize("Admin")]
        public IActionResult Delete(string? Email)
        {
            if (Email == null)
            {
                return NotFound();
            }
            var driver = _dbContext.Drivers.Find(Email);
            if (driver != null)
            {
                return View(driver);
            }
            else
            {
                return RedirectToAction(nameof(DIndex));
            }
        }

        [HttpPost]
        [RoleAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Email, Driver driver)
        {
            
            if (driver != null)
            {
                _dbContext.Drivers.Remove(driver);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(DIndex));
        }


        [HttpGet]
        public IActionResult EnterRental()
        {
            ViewBag.Companies = _dbContext.Company.ToList();
            ViewBag.Vehicles = _dbContext.Vehicle.ToList();
            ViewBag.email = HttpContext.Session?.GetString("Email");
            var email = HttpContext.Session.GetString("Email");
            var rental = new Rental
            {
                IsGST = false,
                Date = DateTime.Now,
                Email = email
            };
            return View(rental);
        }

        [HttpPost]
        public async Task<IActionResult> EnterRental(Rental rental)
        {
            if (ModelState.IsValid)
            {
                rental.Email = HttpContext.Session.GetString("Email");
                if (!rental.IsGST)
                {
                    rental.CompanyId = null;
                }
                _dbContext.Rental.Add(rental);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("DriverDashBoard");
            }
            ViewBag.Companies = _dbContext.Company.ToList();
            ViewBag.Vehicles = _dbContext.Vehicle.ToList();
            return View(rental);
        }

        public IActionResult RentalHistory()
        {
            var driverEmail = HttpContext.Session.GetString("Email");
            var rentals = _dbContext.Rental.Where(r => r.Email == driverEmail).ToList();
            return View(rentals);
        }

        [HttpGet]
        public IActionResult EditRental(int id)
        {
            var rental = _dbContext.Rental.Find(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewBag.Companies = _dbContext.Company.ToList();
            ViewBag.Vehicles = _dbContext.Vehicle.ToList();
            return View(rental);
        }

        [HttpPost]
        public async Task<IActionResult> EditRental(Rental rental)
        {
            if (ModelState.IsValid)
            {
                if (!rental.IsGST)
                {
                    rental.CompanyId = null;
                }
                _dbContext.Rental.Update(rental);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("RentalHistory");
            }
            ViewBag.Companies = _dbContext.Company.ToList();
            ViewBag.Vehicles = _dbContext.Vehicle.ToList();
            return View(rental);
        }

        public IActionResult SalaryHistory()
        {
            var driverEmail = HttpContext.Session.GetString("Email");
            var rentals = _dbContext.Rental.Where(r => r.Email == driverEmail && r.Date >= DateTime.Now.AddDays(-7)).ToList();
            var totalAmount = rentals.Sum(r => r.Amount);
            var uniqueRentalDays = rentals.Select(r => r.Date.Date).Distinct().Count();

            var salary = uniqueRentalDays * 800;

            ViewBag.TotalAmount = totalAmount;
            ViewBag.Salary = salary;
            return View(rentals);
        }

        

    }
}
