using System.Net.Http.Headers;
using Newtonsoft.Json;
using JwtMongoMiddleware.CyptoMiddleware;
using OrderManagementService.Models.Catalog;
using OrderManagementService.Messages;

namespace OrderManagementService.Repositories
{
    public class CartManagementRepository : ICartManagementRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CartManagementRepository> _logger;
        private readonly IJwtTokenFactory _jwtTokenFactory;

        public CartManagementRepository(IHttpClientFactory httpClientFactory, ILogger<CartManagementRepository> logger, IJwtTokenFactory jwtTokenFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _jwtTokenFactory = jwtTokenFactory;
        }

        public async Task<List<OrderItem>> GetCartItemsAsync(string userId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CartClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken(userId));

                var response = await client.GetAsync($"?userId={userId}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<OrderItem>>(content);
                return items ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ErrorMessages.GetCartItemsError}{ex.Message}");
                return [];
            }
        }

        public async Task DeleteCartItemsAsync(string userId, List<string> cartItemIds)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CartClient");
                foreach (var itemId in cartItemIds)
                {
                    var response = await client.DeleteAsync($"?userId={userId}&cartItemId={itemId}");
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ErrorMessages.DeleteCartItemsError}{ex.Message}");
            }
        }

        private string GetToken(string userId)
        {
            return _jwtTokenFactory.GenerateJwt(userId);
        }
    }
}