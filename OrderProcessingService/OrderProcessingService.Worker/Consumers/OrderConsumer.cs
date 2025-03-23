using MassTransit;
using MongoDB.Driver;
using OrderProcessingService.Worker.Messages;

namespace OrderProcessingService.Worker.Consumers;

public class OrderConsumer : IConsumer<OrderMessage>
{
    private readonly IMongoCollection<OrderMessage> _collection;

    public OrderConsumer(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("orders_db");
        _collection = database.GetCollection<OrderMessage>("orders");
    }

    public async Task Consume(ConsumeContext<OrderMessage> context)
    {
        var message = context.Message;
        await _collection.InsertOneAsync(message);
        Console.WriteLine($"✔ Pedido processado: {message.OrderId}");
    }
}
