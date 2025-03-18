using MongoDB.Bson.Serialization.Attributes;

namespace OrderProcessingService.Domain.models
{
    public class Client
    {
        [BsonElement("idClient")]
        public int IdClient { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("document")]
        public string Document { get; set; }
    }
}
