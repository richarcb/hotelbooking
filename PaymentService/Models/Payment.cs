namespace PaymentService.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
        public string PaymentStatus { get; set; }
        public DateTime Date { get; set; }

    }
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        BankTransfer
    }

}
