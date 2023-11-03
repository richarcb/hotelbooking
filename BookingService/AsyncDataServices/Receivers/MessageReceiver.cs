using Azure.Messaging.ServiceBus;
using BookingService.Data;
using BookingService.Dtos;
using BookingService.Services;
using System.Text.Json;

namespace BookingService.AsyncDataServices.Receivers
{
    public class MessageReceiver : IHostedService, IMessageReceiver
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IKeyVaultService _keyVaultService;

        public MessageReceiver(string topicName, string subscriptionName, IServiceProvider serviceProvider, IKeyVaultService keyVaultService)
        {
            _serviceBusClient = new ServiceBusClient(_keyVaultService.GetSecret("ServiceBusConn"));
            _processor = _serviceBusClient.CreateProcessor(topicName, subscriptionName);
            _serviceProvider = serviceProvider;
            _keyVaultService = keyVaultService;
        }
        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Message handler encountered an exception: {args.Exception}.");
            return Task.CompletedTask;
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            using var scope = _serviceProvider.CreateScope();
            var bookingRepo = scope.ServiceProvider.GetRequiredService<IBookingRepo>();
            string body = args.Message.Body.ToString();
            var completedPayment = JsonSerializer.Deserialize<BookingPaymentDto>(body);
            Console.WriteLine("Message received: " + completedPayment);
            var existingBooking = bookingRepo.GetBookingById(completedPayment.OrderId);
            existingBooking.PaymentStatus = completedPayment.PaymentStatus;
            bookingRepo.UpdateBooking(existingBooking);

            var newBooking = JsonSerializer.Serialize(existingBooking);
            Console.WriteLine($"Booking with Id {existingBooking.Id} tagged with PaymentCompleted");
            Console.WriteLine("New booking: " + newBooking);

            await args.CompleteMessageAsync(args.Message);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
            Console.WriteLine("Listening for messages");
            return _processor.StartProcessingAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _processor.StopProcessingAsync(cancellationToken);
        }
    }
}
