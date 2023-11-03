using PaymentService.Models;

namespace PaymentService.Data
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

            if (!context.Payments.Any())
            {
                Console.WriteLine("--> Seeding Data...");
                context.Payments.AddRange(
                    new Payment() { OrderId=1, UserId=1, Date=DateTime.Now, PaymentMethod=PaymentMethod.PayPal, PaymentStatus="Pending" }
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
