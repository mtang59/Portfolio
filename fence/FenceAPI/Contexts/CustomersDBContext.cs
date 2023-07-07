using Microsoft.EntityFrameworkCore;
using FenceWebApp.Models;

namespace FenceWebApp.Data
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }
    }
}
