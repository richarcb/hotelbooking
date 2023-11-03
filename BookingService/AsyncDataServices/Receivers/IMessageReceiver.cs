using Azure.Messaging.ServiceBus;

namespace BookingService.AsyncDataServices.Receivers
{
    public interface IMessageReceiver
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task MessageHandler(ProcessMessageEventArgs args);
        Task StopAsync(CancellationToken cancellationToken);
        Task ErrorHandler(ProcessErrorEventArgs args);
    }
}
