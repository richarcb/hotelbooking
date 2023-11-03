using HotelService.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelService.Data
{
    public interface IHotelsRepo
    {
        bool SaveChanges();
        IEnumerable<Hotel> GetAllHotels();
        IEnumerable<Room> GetAllRoomsForHotel(int hotelId);
        Hotel GetHotelById(int id);
        void CreateHotel(Hotel hotel);
        Room GetRoomByHotel(int hotelId, int roomId);
        bool UpdateHotel(Hotel hotel);
        bool DeleteHotel(int hotelId);
        int CheckRoomAvailability(int hotelId);
    }
}
