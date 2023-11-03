using System.Text.Json.Serialization;

namespace BookingService.Dtos
{
    public class BookingPaymentDto
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("paymentStatus")]
        public string PaymentStatus { get; set; }


    }
}
