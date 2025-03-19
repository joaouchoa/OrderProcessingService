using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderProcessingService.Domain.models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("client")]
        public Client Client { get; set; }

        [BsonElement("items")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [BsonElement("totalAmount")]
        public double TotalAmount => Items.Sum(item => item.Quantity * item.Price);

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
