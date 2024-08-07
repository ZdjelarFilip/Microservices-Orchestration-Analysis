using CatalogManagementService.Models.Catalog;
using CatalogManagementService.Repository;
using MongoDB.Driver;

namespace CatalogMicroservice.Repository;

public class CatalogManagementRepository : ICatalogManagementRepository
{
    private readonly IMongoCollection<CatalogItem> _collection;

    public CatalogManagementRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync()
    {
        return await _collection.Find(FilterDefinition<CatalogItem>.Empty).ToListAsync();
    }

    public async Task<CatalogItem> GetCatalogItemAsync(string catalogItemId)
    {
        var filter = GetByIdFilter(catalogItemId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task InsertCatalogItemAsync(CatalogItem catalogItem)
    {
        await _collection.InsertOneAsync(catalogItem);
    }

    public async Task UpdateCatalogItemAsync(CatalogItem catalogItem)
    {
        var filter = GetByIdFilter(catalogItem.Id);
        var update = Builders<CatalogItem>.Update
            .Set(c => c.Name, catalogItem.Name)
            .Set(c => c.Description, catalogItem.Description)
            .Set(c => c.Price, catalogItem.Price);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteCatalogItemAsync(string catalogItemId)
    {
        var filter = GetByIdFilter(catalogItemId);
        await _collection.DeleteOneAsync(filter);
    }

    private static FilterDefinition<CatalogItem> GetByIdFilter(string id)
    {
        return Builders<CatalogItem>.Filter.Eq(c => c.Id, id);
    }
}