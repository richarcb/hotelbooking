using Azure.Messaging.ServiceBus;
using BookingService.Services;
using PaymentService.Dtos;
using System.Text.Json;

namespace PaymentService.AyncDataServices.Receivers
{
    public class MessageProcessor : IHostedService, IMessageProcessor
    {
        private readonly IKeyVaultService _keyVaulService;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _processor;
        private readonly ServiceBusSender _serviceBusSender;
        private readonly ServiceBusClient _processedServiceBusClient;

        public MessageProcessor(string processedBusConncetionString, string receiverTopicName, string subscriptionName, string processedTopicName, IKeyVaultService keyVaultService)
        {
            _keyVaulService = keyVaultService;
            _serviceBusClient = new ServiceBusClient(_keyVaulService.GetSecret("ServiceBusConn"));
            _processedServiceBusClient = new ServiceBusClient(_keyVaulService.GetSecret("ProcessedOrdersTopicConn"));
            _processor = _serviceBusClient.CreateProcessor(receiverTopicName, subscriptionName);
            _serviceBusSender = _processedServiceBusClient.CreateSender(processedTopicName);
        }
        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            //Console.WriteLine(body);
            var payment = JsonSerializer.Deserialize<PaymentProcessedDto>(body);
            //Azure process
            
            
            payment.PaymentStatus = "Completed";
            var paymentCompleted = JsonSerializer.Serialize(payment);
            
            ServiceBusMessage message = new ServiceBusMessage(paymentCompleted);
            _serviceBusSender.SendMessageAsync(message);
            await args.CompleteMessageAsync(args.Message);
        }
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Message handler encountered an exception: {args.Exception}.");
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            return _processor.StartProcessingAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _processor.StopProcessingAsync(cancellationToken);
        }
    }
}
