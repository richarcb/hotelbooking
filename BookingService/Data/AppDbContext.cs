using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
