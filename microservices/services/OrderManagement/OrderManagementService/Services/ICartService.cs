using OrderManagementService.Messages;
using OrderManagementService.Models.Catalog;

namespace OrderManagementService.Services
{
    public interface ICartService
    {
        Task<ServiceResult<List<OrderItem>>> GetCartItemsAsync(string userId);
        Task<ServiceResult> DeleteCartItemsAsync(string userId, List<string> itemIds);
    }
}
