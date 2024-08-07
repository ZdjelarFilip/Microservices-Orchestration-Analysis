using UserAuthenticationService.Messages;

namespace UserAuthenticationService.Services
{
    public interface IAuthenticationService
    {
        Task<ServiceResult> AuthenticateUserAsync(string email, string password);
        Task<ServiceResult> RegisterUserAsync(string email, string password);
        Task<ServiceResult> ValidateTokenAsync(string email, string token);
    }
}