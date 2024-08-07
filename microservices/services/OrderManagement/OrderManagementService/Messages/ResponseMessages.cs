using OrderManagementService.Models.Catalog;

namespace OrderManagementService.Messages
{
    public static class ResponseMessages
    {
        public const string OrderNotFound = "Order was not found";
        public const string OrderCreatedSuccess = "Order created successfully";
        public const string OrderUpdatedSuccess = "Order updated successfully";
        public const string OrderRemovedSuccess = "Order removed successfully";
        public const string OrderRemovedUnsuccess = "Order removed unsuccessfully";
        public const string CartItemsRemovedSuccess = "Cart items removed successfully";
        public const string GetCartItemsSuccess = "Cart items fetched successfully";
        public const string CartItemsRetrievedSuccess = "Cart items retrieved successfully";
        public const string OrderCreatedCartRemoved = "Order created and cart removed";        
    }
}
