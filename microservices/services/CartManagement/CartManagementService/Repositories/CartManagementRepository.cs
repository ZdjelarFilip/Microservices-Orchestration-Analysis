using CartManagementService.Model;
using CartManagementService.Repository;
using MongoDB.Driver;

namespace CartMicroservice.Repository;

public class CartManagementRepository : ICartManagementRepository
{
    private readonly IMongoCollection<Cart> _collection;

    public CartManagementRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<Cart>(Cart.DocumentName);
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
    {
        var cart = await _collection.Find(c => c.UserId == userId).FirstOrDefaultAsync();
        return cart?.CartItems ?? [];
    }

    public async Task InsertCartItemAsync(string userId, CartItem cartItem)
    {
        var filter = Builders<Cart>.Filter.Where(c => c.UserId == userId);
        var update = Builders<Cart>.Update
            .AddToSet(c => c.CartItems, cartItem)
            .SetOnInsert(c => c.UserId, userId);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }

    public async Task UpdateCartItemAsync(string userId, CartItem cartItem)
    {
        var filter = Builders<Cart>.Filter.And(
            Builders<Cart>.Filter.Eq(c => c.UserId, userId),
            Builders<Cart>.Filter.ElemMatch(c => c.CartItems, ci => ci.CatalogItemId == cartItem.CatalogItemId));

        var update = Builders<Cart>.Update
            .Set("CartItems.$.Name", cartItem.Name)
            .Set("CartItems.$.Price", cartItem.Price)
            .Set("CartItems.$.Quantity", cartItem.Quantity);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteCartItemAsync(string userId, string cartItemId)
    {
        var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
        var update = Builders<Cart>.Update.PullFilter(c => c.CartItems, ci => ci.CatalogItemId == cartItemId);
        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task UpdateCatalogItemAsync(string catalogItemId, string name, decimal price)
    {
        var filter = Builders<Cart>.Filter.ElemMatch(c => c.CartItems, ci => ci.CatalogItemId == catalogItemId);
        var update = Builders<Cart>.Update
            .Set("CartItems.$.Name", name)
            .Set("CartItems.$.Price", price);
        await _collection.UpdateManyAsync(filter, update);
    }

    public async Task DeleteCatalogItemAsync(string catalogItemId)
    {
        var filter = Builders<Cart>.Filter.ElemMatch(c => c.CartItems, ci => ci.CatalogItemId == catalogItemId);
        var update = Builders<Cart>.Update.PullFilter(c => c.CartItems, ci => ci.CatalogItemId == catalogItemId);
        await _collection.UpdateManyAsync(filter, update);
    }
}