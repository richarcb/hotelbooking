using Microsoft.EntityFrameworkCore;
using HotelService.Models;

namespace HotelService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel!)
                .HasForeignKey(r => r.HotelId);
            modelBuilder
                .Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(r => r.Rooms)
                .HasForeignKey(h => h.HotelId);
        }

        //modelBuilder
        //       .Entity<Platform>()
        //        .HasMany(p => p.Commands)
        //        .WithOne(p => p.Platform!)
        //        .HasForeignKey(p => p.PlatformId);
        //modelBuilder
        //    .Entity<Command>()
        //        .HasOne(p => p.Platform)
        //        .WithMany(p => p.Commands)
        //        .HasForeignKey(p => p.PlatformId);
    }
}
