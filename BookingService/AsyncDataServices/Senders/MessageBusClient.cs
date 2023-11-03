using BookingService.Models;
using System;
using Azure.Messaging.ServiceBus;
using System.Text;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;
using BookingService.Dtos;

namespace BookingService.AsyncDataServices.Senders
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _serviceBusSender;


        public MessageBusClient(string serviceBusConnectionString, string topicName)
        {
            _serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
            _serviceBusSender = _serviceBusClient.CreateSender(topicName);
        }

        public async Task SendMessageAsync(BookingPaymentDto booking)
        {


            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string messageBody = JsonSerializer.Serialize(booking, options);

            Console.WriteLine($"Sending message: {messageBody}");
            ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            await _serviceBusSender.SendMessageAsync(message);
            Console.WriteLine($"--> Message to service bus sent");


        }
    }
}
