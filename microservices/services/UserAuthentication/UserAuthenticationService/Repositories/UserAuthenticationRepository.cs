using MongoDB.Driver;
using UserAuthenticationService.Models.User;

namespace UserAuthenticationService.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserAuthenticationRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>(User.DocumentName);
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task InsertUserAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}