using CartManagementService.Messages;
using CartManagementService.Model;

namespace CartManagementService.Services
{
    public interface ICartService
    {
        Task<ServiceResult<IEnumerable<CartItem>>> FetchCartItemsForUserAsync(string userId);
        Task<ServiceResult> AddCartItemAsync(string userId, CartItem cartItem);
        Task<ServiceResult> UpdateCartItemAsync(string userId, CartItem cartItem);
        Task<ServiceResult> RemoveCartItemAsync(string userId, string cartItemId);
        Task<ServiceResult> UpdateCatalogItemAsync(string catalogItemId, string name, decimal price);
        Task<ServiceResult> RemoveCatalogItemAsync(string catalogItemId);
    }
}