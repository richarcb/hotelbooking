using AutoMapper;
using HotelService.Dtos;
using HotelService.Models;

namespace HotelService.Profiles
{
    public class RoomsProfile : Profile
    {
        public RoomsProfile()
        {
            CreateMap<RoomCreateDto, Room>();
            CreateMap<Room, RoomReadDto>();
        }
    }
}
