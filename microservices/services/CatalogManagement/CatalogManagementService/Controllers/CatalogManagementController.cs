using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogManagementService.Models.Catalog;
using CatalogManagementService.Services;

namespace CatalogManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogManagementController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogManagementController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogItems()
        {
            var result = await _catalogService.FetchAllCatalogItemsAsync();

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogItem(string id)
        {
            var result = await _catalogService.FetchCatalogItemByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalogItem([FromBody] CatalogItem catalogItem)
        {
            var result = await _catalogService.AddCatalogItemAsync(catalogItem);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCatalogItem([FromBody] CatalogItem catalogItem)
        {
            var result = await _catalogService.UpdateCatalogItemAsync(catalogItem);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(string id)
        {
            var result = await _catalogService.RemoveCatalogItemAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }
    }
}