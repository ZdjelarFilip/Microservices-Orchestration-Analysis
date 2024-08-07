using OrderManagementService.Models.Catalog;

namespace OrderManagementService.Repositories
{
    public interface ICartManagementRepository
    {
        Task<List<OrderItem>> GetCartItemsAsync(string userId);
        Task DeleteCartItemsAsync(string userId, List<string> listOfIds);
    }
}