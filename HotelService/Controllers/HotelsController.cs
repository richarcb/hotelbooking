using AutoMapper;
using HotelService.Data;
using HotelService.Dtos;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepo _repository; // Assume this service is injected

        public HotelsController(IHotelsRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> GetAllHotels()
        {
            var hotels = _repository.GetAllHotels();
            return Ok(hotels);
        }
        [HttpGet("{hotelId}/rooms")]
        public ActionResult<IEnumerable<Room>> GetRoomsByHotel(int hotelId)
        {
            var rooms = _repository.GetAllRoomsForHotel(hotelId);
            return Ok(rooms);
        }
        [HttpGet("{hotelId}/rooms/{roomId}")]
        public ActionResult<Room> GetRoomByHotel(int hotelId, int roomId)
        {
            return _repository.GetRoomByHotel(hotelId, roomId);
        }

        [HttpGet("{hotelId}/availability")]
        public ActionResult<int> GetAvailableRoomsForHotel(int hotelId)
        {
            var availableRoomId =  _repository.CheckRoomAvailability(hotelId);
            if (availableRoomId == null)
                return NotFound();
            return availableRoomId;
        }
    }
}
