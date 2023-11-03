using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using HotelService.Models;

namespace HotelService.Data
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
        /*
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int StarRating { get; set; }
        public List<Room> Rooms { get; set; }
         */
        private static void SeedData(AppDbContext context)
        {

            if (!context.Hotels.Any())
            {
                Console.WriteLine("--> Seeding data");
                context.Hotels.AddRange(
                    new Hotel() { Name = "Hilton", Description = "Paris Hilton was here", Location = "Oslo", StarRating = 4 },
                    new Hotel() { Name = "Radisson Blue", Description = "Blue hotel yes", Location = "Oslo", StarRating = 4 },
                    new Hotel() { Name = "WackHotel", Description = "This is a wack hotel", Location = "Oslo", StarRating = 2 }
                );
                context.Rooms.AddRange(
                    new Room() { Type = "Standard", NumberOfBeds = 2, Price = 100, HotelId = 1, Available=false },
                    new Room() { Type = "Deluxe", NumberOfBeds = 2, Price = 130, HotelId = 1, Available=true },
                    new Room() { Type = "Suite", NumberOfBeds = 1, Price = 150, HotelId = 1, Available=true },
                    new Room() { Type = "Suite", NumberOfBeds = 1, Price = 150, HotelId = 1, Available = true },
                    new Room() { Type = "Suite", NumberOfBeds = 1, Price = 150, HotelId = 1, Available = true }
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
