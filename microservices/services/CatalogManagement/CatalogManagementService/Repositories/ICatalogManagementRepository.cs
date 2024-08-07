using CatalogManagementService.Models.Catalog;

namespace CatalogManagementService.Repository
{
    public interface ICatalogManagementRepository
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync();
        Task<CatalogItem> GetCatalogItemAsync(string catalogItemId);
        Task InsertCatalogItemAsync(CatalogItem catalogItem);
        Task UpdateCatalogItemAsync(CatalogItem catalogItem);
        Task DeleteCatalogItemAsync(string catalogItemId);
    }
}