using AutoMapper;
using BookingService.Dtos;
using BookingService.Models;

namespace BookingService.Profiles
{
    public class BookingProfiles : Profile 
    {
        public BookingProfiles()
        {
            CreateMap<Booking, BookingReadDto>();
            CreateMap<BookingCreateDto, Booking>();
            CreateMap<Booking, BookingPaymentDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
