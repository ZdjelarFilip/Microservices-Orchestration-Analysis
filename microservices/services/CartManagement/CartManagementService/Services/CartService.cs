using CartManagementService.Messages;
using CartManagementService.Model;
using CartManagementService.Repository;

namespace CartManagementService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartManagementRepository _repository;

        public CartService(ICartManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<CartItem>>> FetchCartItemsForUserAsync(string userId)
        {
            var catalogitems = await _repository.GetCartItemsAsync(userId);

            return ServiceResult.SuccessResult(catalogitems);
        }

        public async Task<ServiceResult> AddCartItemAsync(string userId, CartItem cartItem)
        {
            if (cartItem.Quantity <= 0 || cartItem.Price <= 0)
                return ServiceResult.FailureResult(ErrorMessages.InvalidCartItemDetails);

            await _repository.InsertCartItemAsync(userId, cartItem);
            return ServiceResult.SuccessResult(ResponseMessages.CartItemAddedSuccess);
        }

        public async Task<ServiceResult> UpdateCartItemAsync(string userId, CartItem cartItem)
        {
            if (cartItem.Quantity <= 0 || cartItem.Price <= 0)
                return ServiceResult.FailureResult(ErrorMessages.InvalidCartItemDetails);

            await _repository.UpdateCartItemAsync(userId, cartItem);
            return ServiceResult.SuccessResult(ResponseMessages.CartItemUpdatedSuccess);
        }

        public async Task<ServiceResult> RemoveCartItemAsync(string userId, string cartItemId)
        {
            await _repository.DeleteCartItemAsync(userId, cartItemId);
            return ServiceResult.SuccessResult(ResponseMessages.CartItemRemovedSuccess);
        }

        public async Task<ServiceResult> UpdateCatalogItemAsync(string catalogItemId, string name, decimal price)
        {
            if (price <= 0)
                return ServiceResult.FailureResult(ErrorMessages.InvalidCartItemPrice);

            await _repository.UpdateCatalogItemAsync(catalogItemId, name, price);
            return ServiceResult.SuccessResult(ResponseMessages.CatalogItemUpdatedSuccess);
        }

        public async Task<ServiceResult> RemoveCatalogItemAsync(string catalogItemId)
        {
            await _repository.DeleteCatalogItemAsync(catalogItemId);
            return ServiceResult.SuccessResult(ResponseMessages.CatalogItemRemovedSuccess);
        }
    }
}