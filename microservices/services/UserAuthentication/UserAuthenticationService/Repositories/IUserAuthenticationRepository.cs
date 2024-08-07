using UserAuthenticationService.Models.User;

namespace UserAuthenticationService.Repositories
{
    public interface IUserAuthenticationRepository
    {
        Task<User?> GetUserAsync(string email);
        Task InsertUserAsync(User user);
    }
}