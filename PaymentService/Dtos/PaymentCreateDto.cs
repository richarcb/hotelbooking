using PaymentService.Models;

namespace PaymentService.Dtos
{
    public class PaymentCreateDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
