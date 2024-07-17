using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleASPLogin.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleASPLogin.Controllers
{
    public class RentalController : Controller
    {
        private readonly SimpleDbContext _dbContext;
        private readonly ILogger<DriverController> _logger;

        public RentalController(SimpleDbContext dbContext, ILogger<DriverController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
    }
}

