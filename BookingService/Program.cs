using BookingService.Data;
using BookingService.SyncDataServices.http;
using Microsoft.EntityFrameworkCore;
using BookingService.AsyncDataServices.Senders;
using BookingService.AsyncDataServices.Receivers;
using BookingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*"OrdersProcessed": {
    "Conn": "Endpoint=sb://hotelbookingsb.servicebus.windows.net/;SharedAccessKeyName=rcb-test;SharedAccessKey=JT9BK++w0ctOdxujN940prSi2FE3NXfEI+ASbFe5nNk=;EntityPath=processedorders",
    "Topic": "processedorders",
    "Subscription": "processedorders"
  }*/
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>(
    provider => new MessageBusClient(builder.Configuration["ServiceBusConnectionString"], builder.Configuration["TopicName"]));
builder.Services.AddHttpClient<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IKeyVaultService, KeyVaultService>();

builder.Services.AddSingleton<IHostedService>(provider =>
{
    using var scope = provider.CreateScope();
    var keyVaultService = scope.ServiceProvider.GetRequiredService<IKeyVaultService>();
    return new MessageReceiver(
        builder.Configuration["OrdersProcessed:Topic"],
        builder.Configuration["OrdersProcessed:Subscription"],
        provider,
        keyVaultService
        );
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
PrepDb.PrepPopulation(app);
app.Run();
