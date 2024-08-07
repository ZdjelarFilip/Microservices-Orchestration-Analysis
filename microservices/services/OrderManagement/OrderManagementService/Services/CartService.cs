using OrderManagementService.Messages;
using OrderManagementService.Models.Catalog;
using OrderManagementService.Repositories;

namespace OrderManagementService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartManagementRepository _cartManagementRepository;

        public CartService(ICartManagementRepository cartManagementRepository)
        {
            _cartManagementRepository = cartManagementRepository;
        }

        public async Task<ServiceResult<List<OrderItem>>> GetCartItemsAsync(string userId)
        {
            var items = await _cartManagementRepository.GetCartItemsAsync(userId);
            return ServiceResult.SuccessResult(items, ResponseMessages.CartItemsRetrievedSuccess);
        }

        public async Task<ServiceResult> DeleteCartItemsAsync(string userId, List<string> itemIds)
        {
            if (itemIds == null || itemIds.Count == 0)
            {
                return ServiceResult.FailureResult(ErrorMessages.NoItemsFound);
            }

            await _cartManagementRepository.DeleteCartItemsAsync(userId, itemIds);
            return ServiceResult.SuccessResult(ResponseMessages.CartItemsRemovedSuccess);
        }
    }
}