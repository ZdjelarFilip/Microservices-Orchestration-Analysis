using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderManagementService.Models.Catalog
{
    public class Order
    {
        public static readonly string DocumentName = nameof(Order);

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("items")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; set; }

        [BsonElement("customerId")]
        public string CustomerId { get; set; }

        [BsonElement("processingDate")]
        public DateTime ProcessingDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}