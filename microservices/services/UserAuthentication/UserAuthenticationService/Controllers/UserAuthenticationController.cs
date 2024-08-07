using Microsoft.AspNetCore.Mvc;
using UserAuthenticationService.Models.Request;
using UserAuthenticationService.Services;

namespace UserAuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserAuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UserAuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticationRequest request)
        {
            var result = await _authenticationService.AuthenticateUserAsync(request.Email, request.Password);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationRequest request)
        {
            var result = await _authenticationService.RegisterUserAsync(request.Email, request.Password);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Message);
        }

        [HttpGet("validate")]
        public async Task<IActionResult> Validate([FromQuery] ValidateRequest request)
        {
            var result = await _authenticationService.ValidateTokenAsync(request.Email, request.Token);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Message);
        }
    }
}