using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderManagementService.Models.Catalog
{
    public class OrderItem
    {
        [BsonElement("catalogItemId")]
        public string CatalogItemId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}