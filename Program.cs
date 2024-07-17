using SimpleASPLogin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimpleASPLogin.Models;
using SimpleASPLogin.IService;
using SimpleASPLogin.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        

        string connectionString = @"server=localhost;user=root;password=Noone1903;database=transport;";
        //user = "root",password = "Noone1903",host = "localhost",database = "transport"  @"Data Source=localhost; Initial Catalog=transport; User ID=root; Password=Noone1903"
        builder.Services.AddDbContext<SimpleDbContext>(option => option.UseMySql(connectionString, new MySqlServerVersion(new Version(8,3,0))));

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(2); // Set session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<INotiService, NotiService>();
        var app = builder.Build();
        

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseMiddleware<NoCacheMiddleware>();
        app.UseRouting();

        app.UseSession();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Login}/{id?}");

        app.Run();
    }
    
}