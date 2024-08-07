namespace UserAuthenticationService.Models.Request
{
    public class ValidateRequest
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
