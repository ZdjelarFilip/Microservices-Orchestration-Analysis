using MongoDB.Driver;
using OrderManagementService.Messages;
using OrderManagementService.Models.Catalog;

namespace OrderManagementService.Repositories
{
    public class OrderManagementRepository : IOrderManagementRepository
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderManagementRepository(IMongoDatabase db)
        {
            _collection = db.GetCollection<Order>(Order.DocumentName);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _collection.Find(FilterDefinition<Order>.Empty).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _collection.Find(o => o.CustomerId == userId).ToListAsync();
        }

        public async Task<ServiceResult> CreateOrderAsync(Order order)
        {
            try
            {
                await _collection.InsertOneAsync(order);
                return ServiceResult.SuccessResult(ResponseMessages.OrderUpdatedSuccess);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult(ErrorMessages.OrderCreatedFailure + ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteOrderAsync(string orderId)
        {
            var result = await _collection.DeleteOneAsync(o => o.Id == orderId);
            if (result.DeletedCount == 0)
            {
                return ServiceResult.FailureResult(ResponseMessages.OrderNotFound);
            }

            return ServiceResult.SuccessResult(ResponseMessages.OrderRemovedSuccess);
        }
    }
}