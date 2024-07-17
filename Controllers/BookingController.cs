using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleASPLogin.Filters;
using SimpleASPLogin.Models;
using Microsoft.AspNetCore.Http;
using SimpleASPLogin.IService;

namespace SimpleASPLogin.Controllers
{
    [SessionAuthorize]
    [RoleAuthorize("User")]
    public class BookingController : BaseController
    {
        private readonly SimpleDbContext _dbContext;
        private readonly ILogger<BookingController> _logger;
        private readonly INotiService _notiService;

        public BookingController(SimpleDbContext dbContext, ILogger<BookingController> logger, INotiService notiService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _notiService = notiService;
        }

        [HttpGet]
        public IActionResult Booking()
        {
            var vehicles = _dbContext.Vehicle.ToList();
            ViewBag.Vehicles = vehicles;
            ViewBag.email = HttpContext.Session?.GetString("Email");
            var email = HttpContext.Session.GetString("Email");
            return View();
        }

        [HttpPost]
        public IActionResult Booking(Booking booking)
        {
            var vehicles = _dbContext.Vehicle.ToList();
            ViewBag.Vehicles = vehicles;
            ViewBag.email= HttpContext.Session?.GetString("Email");
            var email = HttpContext.Session?.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login","User"); 
            }

            //var driverEmail = vehicles.DriverEmail;

            //booking.DriverEmail = driverEmail;

            booking.Email = email;

            if (!IsVehicleAvailable(booking.Vehicle_id, booking.BookingDate, booking.BookingTime, booking.RentalDuration))
            {
                ModelState.AddModelError("", "The vehicle is not available at the selected time. Please choose a different time or vehicle.");
                return View(booking);
            }

            if (ModelState.IsValid)
            {
                booking.TotalPrice = CalculatePrice(booking);
                _dbContext.Add(booking);
                _dbContext.SaveChanges();
                var notification = new Noti
                {
                    FromUserEmail = booking.Email,
                    ToUserEmail = booking.DriverEmail,
                    NotiHeader = "Booking",
                    NotiBody = $"Booking Details: \n" +
                               $"Vehicle Type: {booking.Vehicle_Type}\n" +
                               $"Booking Date: {booking.BookingDate}\n" +
                               $"Booking Time: {booking.BookingTime}\n" +
                               $"Pickup Location: {booking.PickupLocation}\n" +
                               $"Delivery Location: {booking.DeliveryLocation}\n" +
                               $"Rental Duration: {booking.RentalDuration}\n" +
                               $"Distance: {booking.Distance}\n" +
                               $"Total Price: {booking.TotalPrice}",
                    CreatedDate = DateTime.Now,
                    Url = ""
                };
                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                return RedirectToAction("BookingSuccess", booking);
            }

            
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            return View(booking);
        }
        [HttpGet]
        public IActionResult BookingSuccess(Booking booking)
        {
            return View(booking);
        }

        [HttpGet]
        public IActionResult MyBookings(string Email)
        {
            var email = HttpContext.Session?.GetString("Email");
            var bookings = _dbContext.Booking.Where(b => b.Email == email).ToList();
            //ViewBag.Bookings = bookings;
            var history = bookings.Where(b => (b.BookingDate + b.BookingTime) < DateTime.Now).ToList();
            var pending = bookings.Where(b => (b.BookingDate + b.BookingTime) >= DateTime.Now).ToList();

            ViewBag.History = history;
            ViewBag.Pending = pending;

            return View();
        }

        [HttpGet]
        public IActionResult CancelBooking(int id)
        {
            var booking = _dbContext.Booking.Find(id);
            if (booking != null)
            {
                _dbContext.Booking.Remove(booking);
                _dbContext.SaveChanges();
                return RedirectToAction("BookingCanceled");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult BookingCanceled()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditBooking(int id)
        {
            var booking = _dbContext.Booking.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        public IActionResult EditBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(booking);
                _dbContext.SaveChanges();
                return RedirectToAction("MyBookings");
            }

            return View(booking);
        }

        public bool IsVehicleAvailable(int vehicle_id, DateTime bookingDate, TimeSpan bookingTime, int rentalDuration)
        {
            DateTime bookingStartDateTime = bookingDate + bookingTime;
            DateTime bookingEndDateTime = bookingStartDateTime.AddHours(rentalDuration);

            var bookings = _dbContext.Booking
                .Where(b => b.Vehicle_id == vehicle_id)
                .Select(b => new
                {
                    b.BookingDate,
                    b.BookingTime,
                    b.RentalDuration
                })
                .ToList();

            foreach (var b in bookings)
            {
                DateTime existingBookingStart = b.BookingDate + b.BookingTime;
                DateTime existingBookingEnd = existingBookingStart.AddHours(b.RentalDuration);

                bool isOverlapping = bookingStartDateTime < existingBookingEnd && existingBookingStart < bookingEndDateTime;
                if (isOverlapping)
                {
                    return false;
                }
            }

            return true;
        }

        

        private double CalculatePrice(Booking booking)
        {
            double basePrice = 0;
            double freeDuration = 0;
            double freeDistance = 0;
            double extraKmRate = 0;
            double additionalTimeRate = 0;

            switch (booking.Vehicle_Type)
            {
                case "Tata Ace":
                    basePrice = 800;
                    freeDuration = 2;
                    freeDistance = 8;
                    extraKmRate = 20;
                    additionalTimeRate = 4;
                    break;
                case "Eicher":
                    basePrice = 900;
                    freeDuration = 2.5;
                    freeDistance = 9;
                    extraKmRate = 22;
                    additionalTimeRate = 4.5;
                    break;
                case "Dost":
                    basePrice = 850;
                    freeDuration = 2;
                    freeDistance = 7;
                    extraKmRate = 18;
                    additionalTimeRate = 4;
                    break;
                case "SML 22 Feet Truck":
                    basePrice = 1500;
                    freeDuration = 4;
                    freeDistance = 15;
                    extraKmRate = 30;
                    additionalTimeRate = 6;
                    break;
                case "SML 17 Feet Truck":
                    basePrice = 1500;
                    freeDuration = 4;
                    freeDistance = 15;
                    extraKmRate = 30;
                    additionalTimeRate = 6;
                    break;
            }

            double extraKm = Math.Max(0, booking.Distance - freeDistance);
            double extraTime = Math.Max(0, booking.RentalDuration - freeDuration);

            double totalPrice = basePrice + (extraKm * extraKmRate) + (extraTime * additionalTimeRate * 60); 

            return totalPrice;
        }
    }
}

