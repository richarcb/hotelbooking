using BookingService.Models;

namespace BookingService.Data
{
    public interface IBookingRepo
    {
        public bool CreateBooking(Booking booking);
        public Booking GetBookingById(int id);
        public bool UpdateBooking(Booking booking);
        public bool DeleteBooking(int id);
        public Booking GetBookingByUserId(int userId);
        public IEnumerable<Booking> GetBookingsByHotelId(int hotelId);
        public bool BookingExists(int bookingId);
        public bool SaveChanges();
        public IEnumerable<Booking> GetAllBookings();

    }
}
