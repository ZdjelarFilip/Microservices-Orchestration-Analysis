namespace UserAuthenticationService.Models.Request
{
    public class AuthenticationRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}