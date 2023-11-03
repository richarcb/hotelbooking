using HotelService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace HotelService.Data
{
    public class RoomsRepo : IRoomsRepo
    {
        private readonly AppDbContext _context;

        public RoomsRepo(AppDbContext context)
        {
            _context = context;
        }

        

        public void CreateRoom(int hotelId, Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .SingleOrDefault(h => h.Id == hotelId);
            if(hotel != null)
            {
                hotel.Rooms.Add(room);
                room.HotelId = hotelId;
                _context.Rooms.Add(room);
            }
        }

        public IEnumerable<Room> GetAllRoomsForHotel(int hotelId)
        {
            return _context.Rooms.Where(r => r.HotelId == hotelId).ToList();
        }

        public IEnumerable<Room> GetAvailableRoomsByHotelId(int hotelId)
        {
            return _context.Rooms.Where(r => r.HotelId == hotelId && r.Available == true).ToList();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Room> GetRoomsByHotelId(int hotelId)
        {
            return _context.Rooms.Where(r => r.Id == hotelId).ToList();
        }

        public IEnumerable<Room> GetRoomsByType(string type)
        {
            return _context.Rooms.Where(r => r.Type == type).ToList();
        }

        public bool HotelExists(int hotelId)
        {
            return _context.Hotels.Any(h => h.Id == hotelId);


        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }
    }
}
