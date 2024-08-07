using OrderManagementService.Messages;
using OrderManagementService.Models.Catalog;
using OrderManagementService.Repositories;

namespace OrderManagementService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderManagementRepository _orderRepository;
        private readonly ICartService _cartService;

        public OrderService(IOrderManagementRepository orderRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        public async Task<ServiceResult<IEnumerable<Order>>> FetchAllOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();

            return ServiceResult.SuccessResult(orders);
        }

        public async Task<ServiceResult<IEnumerable<Order>>> FetchOrderByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            return ServiceResult.SuccessResult(orders);
        }

        public async Task<ServiceResult> CreateOrderAsync(string userId)
        {
            var cartResult = await _cartService.GetCartItemsAsync(userId);
            if (!cartResult.Success || cartResult.Data == null || cartResult.Data.Count == 0)
            {
                return ServiceResult.FailureResult(ErrorMessages.MissingCartItemsError);
            }

            var itemIds = cartResult.Data.Select(item => item.CatalogItemId).ToList();
            var totalPrice = cartResult.Data.Sum(item => item.Price * item.Quantity);
            var order = new Order
            {
                CustomerId = userId,
                Items = cartResult.Data,
                TotalPrice = totalPrice,
                Status = "Completed",
                ProcessingDate = DateTime.UtcNow
            };

            var createOrderResult = await _orderRepository.CreateOrderAsync(order);
            if (createOrderResult.Success)
            {
                await _cartService.DeleteCartItemsAsync(userId, itemIds);
                return ServiceResult.SuccessResult(ResponseMessages.OrderCreatedCartRemoved);
            }
            else
            {
                return ServiceResult.FailureResult(ResponseMessages.OrderRemovedUnsuccess);
            }
        }


        public async Task<ServiceResult> RemoveOrderAsync(string orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}