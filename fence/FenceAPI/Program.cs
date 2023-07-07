using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FenceWebApp.Data;

namespace FenceWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<CustomersDbContext>();

                // Apply any pending migrations and seed data
                dbContext.Database.Migrate();
                // You can add any additional logic here, such as seeding initial data
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5001")
                .ConfigureServices((hostContext, services) =>
                {
                    // Other service registrations...

                    services.AddDbContext<CustomersDbContext>(options =>
                    { // DISABLED SSL 
                        options.UseSqlServer("Server=MICHAEL;Database=FenceAppDB;Trusted_Connection=true;TrustServerCertificate=true;");
                    });

                    // Other service registrations...
                })
                .UseStartup<Program>();
        
        public void Configure(IApplicationBuilder app)
        {
            // Configure middleware here
        }
    }
}
