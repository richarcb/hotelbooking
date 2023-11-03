using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {

            CreateMap<PaymentCreateDto, Payment>();
            
        }
    }
}
