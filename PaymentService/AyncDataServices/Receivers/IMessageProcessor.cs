using Azure.Messaging.ServiceBus;

namespace PaymentService.AyncDataServices.Receivers
{
    public interface IMessageProcessor
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task MessageHandler(ProcessMessageEventArgs args);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
