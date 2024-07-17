using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleASPLogin.Filters;
using SimpleASPLogin.Models;
using System.Linq;

namespace SimpleASPLogin.Controllers
{
    [SessionAuthorize]
    public class VehicleController : BaseController
    {
        private readonly SimpleDbContext _dbContext;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(SimpleDbContext dbContext, ILogger<VehicleController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public IActionResult VIndex()
        {
            var vehicles = _dbContext.Vehicle.ToList();
            ViewBag.Role= HttpContext.Session?.GetString("Role");
            return View(vehicles);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _dbContext.Vehicle.FirstOrDefault(v => v.Vehicle_id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [RoleAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Vehicle.Add(vehicle);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(VIndex));
            }
            return View(vehicle);
        }

        [HttpGet]
        [RoleAuthorize("Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var vehicle = _dbContext.Vehicle.Find(id);
                if (vehicle != null)
                {
                    return View(vehicle);
                }
                return View(vehicle);

            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleAuthorize("Admin")]
        public IActionResult Edit(int id, Vehicle vehicle)
        {
            if (id == vehicle.Vehicle_id)
            {
                _dbContext.Update(vehicle);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(VIndex));
                
            }
            else
            {
                return View(vehicle);   
            }
            
        }

        [HttpGet]
        [RoleAuthorize("Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = _dbContext.Vehicle.Find(id);
            if (vehicle != null)
            {
                return View(vehicle);
            }
            return View(vehicle);
        }

        [HttpPost]
        [RoleAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Vehicle vehicle)
        {
            //var vehicle = _dbContext.Vehicle.Find(id);
            if (vehicle != null)
            {
                _dbContext.Vehicle.Remove(vehicle);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(VIndex));
        }

        


    }
}
