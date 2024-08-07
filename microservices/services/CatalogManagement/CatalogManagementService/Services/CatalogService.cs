using CatalogManagementService.Messages;
using CatalogManagementService.Models.Catalog;
using CatalogManagementService.Repository;

namespace CatalogManagementService.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogManagementRepository _catalogRepository;

        public CatalogService(ICatalogManagementRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<ServiceResult<IEnumerable<CatalogItem>>> FetchAllCatalogItemsAsync()
        {
            var catalogitems = await _catalogRepository.GetCatalogItemsAsync();

            return ServiceResult.SuccessResult(catalogitems);
        }

        public async Task<ServiceResult<CatalogItem>> FetchCatalogItemByIdAsync(string id)
        {
            var catalogitems = await _catalogRepository.GetCatalogItemAsync(id);

            return ServiceResult.SuccessResult(catalogitems);
        }

        public async Task<ServiceResult> AddCatalogItemAsync(CatalogItem catalogItem)
        {
            if (string.IsNullOrEmpty(catalogItem.Name) || catalogItem.Price <= 0)
            {
                return ServiceResult.FailureResult(ErrorMessages.InvalidCatalogItemDetails);
            }

            await _catalogRepository.InsertCatalogItemAsync(catalogItem);
            return ServiceResult.SuccessResult(ResponseMessages.CatalogItemAddedSuccess);
        }

        public async Task<ServiceResult> UpdateCatalogItemAsync(CatalogItem catalogItem)
        {
            if (string.IsNullOrEmpty(catalogItem.Name) || catalogItem.Price <= 0)
            {
                return ServiceResult.FailureResult(ErrorMessages.InvalidCatalogItemDetails);
            }

            await _catalogRepository.UpdateCatalogItemAsync(catalogItem);
            return ServiceResult.SuccessResult(ResponseMessages.CatalogItemUpdatedSuccess);
        }

        public async Task<ServiceResult> RemoveCatalogItemAsync(string id)
        {
            await _catalogRepository.DeleteCatalogItemAsync(id);
            return ServiceResult.SuccessResult(ResponseMessages.CatalogItemRemovedSuccess);
        }
    }
}