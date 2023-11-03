using System.ComponentModel.DataAnnotations.Schema;

namespace HotelService.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }  // e.g., Suite, Deluxe, Standard
        public decimal Price { get; set; }
        public int NumberOfBeds { get; set; }
        public int HotelId { get; set; }
        public bool Available { get; set; }
        public Hotel Hotel { get; set; }
    }
}
