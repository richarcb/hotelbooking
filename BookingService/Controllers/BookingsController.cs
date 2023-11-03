using AutoMapper;
using BookingService.AsyncDataServices.Senders;
using BookingService.Data;
using BookingService.Dtos;
using BookingService.Models;
using BookingService.SyncDataServices.http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IBookingRepo _repository;
        private readonly IMapper _mapper;


        public BookingsController(IAvailabilityService availabilityService, IBookingRepo repository, IMapper mapper, IMessageBusClient messageBusClient) 
        {
            _availabilityService = availabilityService;
            _messageBusClient = messageBusClient;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAllBookings()
        {
            var bookings = _repository.GetAllBookings();
            return Ok(bookings);
        }
        [HttpGet("users/{userId}")]
        public ActionResult<Booking> GetBookingByUser(int userId)
        {
            
            var booking = _repository.GetBookingByUserId(userId);
            if(booking == null)
                return NotFound();
            return Ok(booking);
        }
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(BookingCreateDto bookingCreateDto)
        {
            var bookingExists = _repository.GetBookingByUserId(bookingCreateDto.UserId);
            if(bookingExists != null)
                return BadRequest("Booking made by user already exists.");
            var availableRoom = await _availabilityService.GetRoomAvailability(bookingCreateDto.HotelId);
            if(availableRoom == null)
                return NotFound("No available room was found for the hotel.");
            Console.WriteLine($"Booking room with id: {availableRoom}");
            
            var bookingModel = _mapper.Map<Booking>(bookingCreateDto);
            bookingModel.RoomId = availableRoom;
            _repository.CreateBooking(bookingModel);
            var bookingPaymentModel = _mapper.Map<BookingPaymentDto>(bookingModel);
            _messageBusClient.SendMessageAsync(bookingPaymentModel);
            
            return Ok(_mapper.Map<BookingReadDto>(bookingModel));
        }
        
    }
}
