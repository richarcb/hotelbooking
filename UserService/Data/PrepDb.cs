using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            
            if (!context.Users.Any())
            {
                Console.WriteLine("--> Seeding Data...");
                context.Users.AddRange(
                    new User() { Name = "Tommy Lee", Username = "tommylee69", PasswordHash = "test123"},
                    new User() { Name = "Morten Ramm", Username = "Mortenxd", PasswordHash = "123456morten" },
                    new User() { Name = "Kanye West", Username = "ye", PasswordHash = "Yandhi123!" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data.");
            }
        }
    }
}
