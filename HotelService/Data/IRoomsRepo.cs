using HotelService.Models;

namespace HotelService.Data
{
    public interface IRoomsRepo
    {
        bool SaveChanges();
        IEnumerable<Room> GetAllRoomsForHotel(int hotelId);
        Room GetRoomById(int id);
        void CreateRoom(int hotelId, Room room);
        bool HotelExists(int hotelId);
        IEnumerable<Room> GetRoomsByHotelId(int hotelId); //bygg senere
        IEnumerable<Room> GetAvailableRoomsByHotelId(int hotelId); //bygg senere
        IEnumerable<Room> GetRoomsByType(string type); //bygg senere

        

    }
}
