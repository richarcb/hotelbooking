namespace HotelService.Dtos
{
    public class RoomReadDto
    {
        public int Id { get; set; }
        public string Type { get; set; }  // e.g., Suite, Deluxe, Standard
        public decimal Price { get; set; }
        public int NumberOfBeds { get; set; }

        public int HotelId { get; set; }
    }
}
