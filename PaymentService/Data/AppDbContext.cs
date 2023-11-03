using Microsoft.EntityFrameworkCore;
using PaymentService.Models;
using System.Collections.Generic;

namespace PaymentService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
    }
}
