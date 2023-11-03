namespace BookingService.Dtos
{
    public class BookingCreateDto
    {
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public string PaymentStatus { get; set; }
    }
}
