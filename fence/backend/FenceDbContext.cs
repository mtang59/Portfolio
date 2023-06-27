using Microsoft.EntityFrameworkCore;

namespace FenceApp
{
    public class FenceDbContext : DbContext
    {

        public DbSet<SketchData> Sketches { get; set; }
        public DbSet<LengthData> Lengths { get; set; }
        public DbSet<CustomerData> Customers { get; set; }
        public DbSet<MaterialData> Materials { get; set; }

        public FenceDbContext(DbContextOptions<FenceDbContext> options) : base(options)
        {
        }

        // parameterless constructor for "Unable to create an object of type 'FenceDbContext'"
        public FenceDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, or any other model-specific configurations
            // For example:
            // modelBuilder.Entity<SketchData>().HasKey(s => s.Id);
            // modelBuilder.Entity<SketchData>().HasOne(s => s.Length).WithOne(l => l.Sketch).HasForeignKey<LengthData>(l => l.SketchId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
