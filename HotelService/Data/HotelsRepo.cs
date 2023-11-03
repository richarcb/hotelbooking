using HotelService.Models;

namespace HotelService.Data
{
    public class HotelsRepo : IHotelsRepo
    {
        private readonly AppDbContext _context;
        public HotelsRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateHotel(Hotel hotel)
        {
            if (hotel == null)
                throw new ArgumentNullException(nameof(hotel));
            _context.Hotels.Add(hotel);
        }

        public bool DeleteHotel(int hotelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotel> GetAllHotels()
        {
            return _context.Hotels.ToList();
        }

        public IEnumerable<Room> GetAllRoomsForHotel(int hotelId)
        {
            return _context.Rooms.Where(r => r.HotelId == hotelId).ToList();

        }

        public Hotel GetHotelById(int id)
        {
            return _context.Hotels.FirstOrDefault(h => h.Id == id);
        }

        public Room GetRoomByHotel(int hotelId, int roomId)
        {
            return _context.Rooms.FirstOrDefault(h => h.HotelId == hotelId && h.Id == roomId);
        }
        public int CheckRoomAvailability(int hotelId)
        {
            var roomId = _context.Rooms.FirstOrDefault(r => r.HotelId == hotelId && r.Available == true).Id;
            return roomId;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public bool UpdateHotel(Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}
