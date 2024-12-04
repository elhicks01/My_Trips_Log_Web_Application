using Microsoft.EntityFrameworkCore;

namespace My_Trip_Log.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>().Property(t => t.Destination).IsRequired();
            modelBuilder.Entity<Trip>().Property(t => t.StartDate).IsRequired();
            modelBuilder.Entity<Trip>().Property(t => t.EndDate).IsRequired();
        }
    }
}
