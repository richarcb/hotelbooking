using AutoMapper;
using HotelService.Data;
using HotelService.Dtos;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomsRepo _repository;
        private readonly IMapper _mapper;

        public RoomsController(IRoomsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{hotelId}")]
        public ActionResult GetRoomsByHotel(int hotelId)
        {
            var rooms = _repository.GetAllRoomsForHotel(hotelId);
            return Ok(rooms);
        }
        [HttpPost("{hotelId}")]
        public ActionResult<RoomReadDto> CreateRoomForHotel(int hotelId, RoomCreateDto roomCreateDto)
        {
            if (!_repository.HotelExists(hotelId))
                return NotFound();

            var room = _mapper.Map<Room>(roomCreateDto);
            _repository.CreateRoom(hotelId, room);
            _repository.SaveChanges();

            var roomReadDto = _mapper.Map<RoomReadDto>(room);

            return roomReadDto;
        }

       
    }
}
