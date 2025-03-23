using MassTransit;
using MongoDB.Driver;
using OrderProcessingService.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["MongoDb:ConnectionString"] ?? "mongodb://mongodb:27017";
    return new MongoClient(connectionString);
});

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("orders", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Build().Run();
