using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FenceWebApp.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;



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
                .UseUrls("http://localhost:5042", "http://192.168.0.105:5043") // available ports
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMvc();
                    services.AddControllers();
                    // Other service registrations...
                    services.AddCors(options =>
                        {
                            options.AddDefaultPolicy(builder =>
                            {
                                builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                            });
                        });


                    services.AddDbContext<CustomersDbContext>(options =>
                    { // DISABLED SSL 
                        options.UseSqlServer("Server=MICHAEL;Database=FenceAppDB;Trusted_Connection=true;TrustServerCertificate=true;");
                    });

                    // Other service registrations...
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .UseStartup<Program>();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(); // call after UseRouting and before UseEndpoints.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // This line enables attribute routing.
            });
        }
    }
}
