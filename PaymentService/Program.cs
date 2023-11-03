using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.AyncDataServices.Receivers;
using BookingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IKeyVaultService, KeyVaultService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IHostedService>(provider =>
{
    using var scope = provider.CreateScope();
    var keyVaultService = scope.ServiceProvider.GetRequiredService<IKeyVaultService>();
    return new MessageProcessor(
        builder.Configuration["ProcessedTopicConn"],
        builder.Configuration["ReceiverTopicName"],
        builder.Configuration["SubscriptionName"],
        builder.Configuration["ProcessedTopicName"],
        keyVaultService
        );

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();

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
