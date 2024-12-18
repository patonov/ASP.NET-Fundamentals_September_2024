using CinemaApp.Web.Data;
using CinemaApp.Web.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionStr = builder.Configuration.GetConnectionString("SqlServer");

            // Add services to the container.
            builder.Services.AddDbContext<CinemaDbContext>(
                options => 
                {
                    options.UseSqlServer(connectionStr);
                }
            );

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();                
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.ApplyMigrations();

            app.Run();
        }
    }
}
