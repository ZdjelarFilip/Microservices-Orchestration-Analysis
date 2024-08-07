using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CartManagementService.Model;

public class Cart
{
    public static readonly string DocumentName = nameof(Cart);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}