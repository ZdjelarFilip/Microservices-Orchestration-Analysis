using OrderManagementService.Messages;
using OrderManagementService.Models.Catalog;

namespace OrderManagementService.Repositories
{
    public interface IOrderManagementRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string orderId);
        Task<ServiceResult> CreateOrderAsync(Order order);
        Task<ServiceResult> DeleteOrderAsync(string orderId);
    }
}