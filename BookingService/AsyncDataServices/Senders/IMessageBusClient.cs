using BookingService.Dtos;
using BookingService.Models;

namespace BookingService.AsyncDataServices.Senders
{
    public interface IMessageBusClient
    {
        Task SendMessageAsync(BookingPaymentDto booking);
    }
}
