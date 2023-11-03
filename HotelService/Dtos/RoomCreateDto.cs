namespace HotelService.Dtos
{
    public class RoomCreateDto
    {
        public string Type { get; set; }  // e.g., Suite, Deluxe, Standard
        public decimal Price { get; set; }
        public int NumberOfBeds { get; set; }
        public bool Available { get; set; }
    }
}
