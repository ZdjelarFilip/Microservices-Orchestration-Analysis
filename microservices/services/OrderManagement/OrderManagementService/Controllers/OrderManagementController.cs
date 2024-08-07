using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementService.Services;

namespace OrderManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public partial class OrderManagementController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderManagementController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _orderService.FetchAllOrdersAsync();

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpGet("ByUserId/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var result = await _orderService.FetchOrderByUserIdAsync(userId);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromQuery] string userId)
        {
            var result = await _orderService.CreateOrderAsync(userId);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });

            }

            return Ok(new { result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var result = await _orderService.RemoveOrderAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });

            }

            return Ok(new { result.Message });
        }
    }
}