using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using JwtMongoMiddleware.CyptoMiddleware;

namespace UserAuthenticationService.Models.User
{
    public class User
    {
        public static readonly string DocumentName = nameof(User);

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        private string Password { get; set; }

        [BsonElement("salt")]
        private string Salt { get; set; }

        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }

        public User() {}

        public void InitializePassword(string password, ICryptoSaltHasher cryptoSaltHasher)
        {
            Salt = cryptoSaltHasher.GenerateSalt();
            Password = cryptoSaltHasher.GenerateHash(password, Salt);
        }

        public bool IsPasswordValid(string password, ICryptoSaltHasher cryptoSaltHasher)
        {
            return Password == cryptoSaltHasher.GenerateHash(password, Salt);
        }        
    }
}