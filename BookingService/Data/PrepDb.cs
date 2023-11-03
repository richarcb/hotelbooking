using BookingService.Models;

namespace BookingService.Data
{
    public class PrepDb
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

            if (!context.Bookings.Any())
            {
                Console.WriteLine("--> Seeding data");
                context.Bookings.AddRange(
                    new Booking() { HotelId=1, RoomId=1, UserId=1, CheckInDate=DateTime.Now, CheckOutDate=DateTime.Now.AddDays(2), PaymentStatus="Pending"},
                    new Booking() { HotelId = 1, RoomId = 2, UserId = 2, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), PaymentStatus = "Pending" },
                    new Booking() { HotelId = 2, RoomId = 2, UserId = 3, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), PaymentStatus = "Pending" }
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
