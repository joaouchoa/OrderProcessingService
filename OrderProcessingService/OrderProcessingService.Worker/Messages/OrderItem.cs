namespace OrderProcessingService.Worker.Messages
{
    public record OrderItem(string Product, int Quantity, decimal UnitPrice);
}
