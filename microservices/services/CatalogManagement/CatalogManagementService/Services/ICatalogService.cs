using CatalogManagementService.Messages;
using CatalogManagementService.Models.Catalog;

namespace CatalogManagementService.Services
{
    public interface ICatalogService
    {
        Task<ServiceResult<IEnumerable<CatalogItem>>> FetchAllCatalogItemsAsync();
        Task<ServiceResult<CatalogItem>> FetchCatalogItemByIdAsync(string id);
        Task<ServiceResult> AddCatalogItemAsync(CatalogItem catalogItem);
        Task<ServiceResult> UpdateCatalogItemAsync(CatalogItem catalogItem);
        Task<ServiceResult> RemoveCatalogItemAsync(string id);
    }
}