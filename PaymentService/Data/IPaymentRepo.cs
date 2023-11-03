using PaymentService.Models;

namespace PaymentService.Data
{
    public interface IPaymentRepo
    {
        Task<Payment> GetPaymentAsync(int paymentId);
        Task<IEnumerable<Payment>> GetAllPayments();
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int paymentId);
    }
}
