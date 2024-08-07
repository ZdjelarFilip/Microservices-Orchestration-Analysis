using Microsoft.AspNetCore.Mvc;
using CartManagementService.Services;
using CartManagementService.Model;

namespace CartManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartManagementController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartManagementController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemsAsync([FromQuery] string userId)
        {
            var result = await _cartService.FetchCartItemsForUserAsync(userId);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItemAsync([FromQuery] string userId, [FromBody] CartItem cartItem)
        {
            var result = await _cartService.AddCartItemAsync(userId, cartItem);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItemAsync([FromQuery] string userId, [FromBody] CartItem cartItem)
        {
            var result = await _cartService.UpdateCartItemAsync(userId, cartItem);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItemAsync([FromQuery] string userId, [FromQuery] string cartItemId)
        {
            var result = await _cartService.RemoveCartItemAsync(userId, cartItemId);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpPut("updateCatalogItem")]
        public async Task<IActionResult> UpdateCatalogItemAsync([FromQuery] string catalogItemId, [FromQuery] string name, [FromQuery] decimal price)
        {
            var result = await _cartService.UpdateCatalogItemAsync(catalogItemId, name, price);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpDelete("deleteCatalogItem")]
        public async Task<IActionResult> DeleteCatalogItemAsync([FromQuery] string catalogItemId)
        {
            var result = await _cartService.RemoveCatalogItemAsync(catalogItemId);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }
    }
}