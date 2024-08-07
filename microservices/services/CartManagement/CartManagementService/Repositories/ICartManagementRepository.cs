using CartManagementService.Model;

namespace CartManagementService.Repository
{
    public interface ICartManagementRepository
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);
        Task InsertCartItemAsync(string userId, CartItem cartItem);
        Task UpdateCartItemAsync(string userId, CartItem cartItem);
        Task DeleteCartItemAsync(string userId, string cartItemId);
        Task UpdateCatalogItemAsync(string catalogItemId, string name, decimal price);
        Task DeleteCatalogItemAsync(string catalogItemId);
    }
}