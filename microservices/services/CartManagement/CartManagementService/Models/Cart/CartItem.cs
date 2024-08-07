using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartManagementService.Model;

public class CartItem
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CatalogItemId { get; init; }

    [StringLength(100, MinimumLength = 1)]
    [BsonElement("name")]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    [DefaultValue(1)]
    [BsonElement("price")]
    public decimal Price { get; set; }
    
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    [BsonElement("quantity")]
    public int Quantity { get; set; }
}