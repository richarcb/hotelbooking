using AutoMapper;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class BookingRepo : IBookingRepo
    {
        private readonly AppDbContext _context;

        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool BookingExists(int bookingId)
        {
            throw new NotImplementedException();
        }

        public bool CreateBooking(Booking booking)
        {
            if(booking == null)
                throw new ArgumentNullException(nameof(booking));
            _context.Bookings.Add(booking);
            return SaveChanges();
        }

        public bool DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
                throw new NotImplementedException();
            _context.Remove(booking); 
            return SaveChanges();
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Booking> GetBookingsByHotelId(int hotelId)
        {
            return _context.Bookings.Where(b => b.HotelId == hotelId).ToList();
        }

        public Booking GetBookingByUserId(int userId)
        {
            return _context.Bookings.FirstOrDefault(b => b.UserId == userId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UpdateBooking(Booking updatedBooking)
        {
            _context.Entry(updatedBooking).State = EntityState.Modified;
            return SaveChanges();
        }
    }
}
