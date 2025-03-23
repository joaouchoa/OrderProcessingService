

namespace OrderProcessingService.Worker.Messages
{
    public record OrderMessage(string OrderId, string ClientId, List<OrderItem> Items);
}
