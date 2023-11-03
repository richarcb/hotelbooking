using System.Text.Json.Serialization;

namespace PaymentService.Dtos
{
    public class PaymentProcessedDto
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("paymentStatus")]
        public string PaymentStatus { get; set; }
    }
}
