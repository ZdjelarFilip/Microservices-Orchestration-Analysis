using OrderManagementService.Messages;
using OrderManagementService.Models.Catalog;

namespace OrderManagementService.Services
{
    public interface IOrderService
    {
        Task<ServiceResult<IEnumerable<Order>>> FetchAllOrdersAsync();
        Task<ServiceResult<IEnumerable<Order>>> FetchOrderByUserIdAsync(string userId);
        Task<ServiceResult> CreateOrderAsync(string userId);
        Task<ServiceResult> RemoveOrderAsync(string orderId);
    }
}