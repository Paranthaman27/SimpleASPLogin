using Microsoft.EntityFrameworkCore;
using SimpleASPLogin.Models;
namespace SimpleASPLogin
{
	public class SimpleDbContext:DbContext
	{
        

        public DbSet<User> login { get; set; }
        public DbSet<Driver> Drivers { get; set; }
		public DbSet<Company> Company { get; set; }
		public DbSet<Vehicle> Vehicle { get; set; }
		public DbSet<Booking> Booking { get; set; }
        //public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Rental> Rental { get; set; }
		public DbSet<Noti> Notifications { get; set; }
        public DbSet<ViewNotification> viewnotification { get; set; }

        public SimpleDbContext(DbContextOptions<SimpleDbContext> options) : base(options)
		{

		}
	}




}
