using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CatalogManagementService.Models.Catalog
{
    public class CatalogItem
    {
        public static readonly string DocumentName = nameof(CatalogItem);

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }

        [BsonRequired]
        [BsonElement("name")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        [Range(0.01, double.MaxValue)]
        [DefaultValue(1)]
        public decimal Price { get; set; }
    }
}