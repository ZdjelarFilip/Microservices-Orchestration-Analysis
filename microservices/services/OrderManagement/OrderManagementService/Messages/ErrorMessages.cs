namespace OrderManagementService.Messages
{
    public static class ErrorMessages
    {
        public const string MissingCartItemsError = "No cart items to create an order.";
        public const string CreateOrderError = "Failed to create order.";
        public const string GetCartItemsError = "Failed to retrieve cart items.";
        public const string DeleteCartItemsError = "Failed to delete cart items.";
        public const string OrderCreatedFailure = "Order was not created due to failure";
        public const string NoItemsFound = "No items specified for deletion";
        public const string InvalidConfiguration = "Configuration is invalid";
    }
}
