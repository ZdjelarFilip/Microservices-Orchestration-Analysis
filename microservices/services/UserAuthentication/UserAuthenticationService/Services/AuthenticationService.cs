using UserAuthenticationService.Repositories;
using UserAuthenticationService.Models.User;
using UserAuthenticationService.Messages;
using JwtMongoMiddleware.CyptoMiddleware;

namespace UserAuthenticationService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserAuthenticationRepository _repository;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly ICryptoSaltHasher _cryptoSaltHasher;

        public AuthenticationService(IUserAuthenticationRepository repository, IJwtTokenFactory jwtTokenFactory, ICryptoSaltHasher cryptoSaltHasher)
        {
            _repository = repository;
            _jwtTokenFactory = jwtTokenFactory;
            _cryptoSaltHasher = cryptoSaltHasher;
        }

        public async Task<ServiceResult> AuthenticateUserAsync(string email, string password)
        {
            var user = await _repository.GetUserAsync(email);

            if (user == null || !user.IsPasswordValid(password, _cryptoSaltHasher))
            {
                return ServiceResult.FailureResult(ErrorMessages.InvalidCredentials);
            }

            string jwt = _jwtTokenFactory.GenerateJwt(user.Id);
            return ServiceResult.SuccessResult(jwt);
        }

        public async Task<ServiceResult> RegisterUserAsync(string email, string password)
        {
            var existingUser = await _repository.GetUserAsync(email);

            if (existingUser != null)
            {
                return ServiceResult.FailureResult(ErrorMessages.UserAlreadyExists);
            }

            var newUser = new User { Email = email };
            newUser.InitializePassword(password, _cryptoSaltHasher);
            await _repository.InsertUserAsync(newUser);

            return ServiceResult.SuccessResult(ResponseMessages.RegistrationSuccess);
        }

        public async Task<ServiceResult> ValidateTokenAsync(string email, string token)
        {
            var user = await _repository.GetUserAsync(email);

            if (user == null)
            {
                return ServiceResult.FailureResult(ErrorMessages.InvalidToken);
            }

            var userId = _jwtTokenFactory.ValidateJwt(token);
            if (userId != user.Id)
            {
                return ServiceResult.FailureResult(ErrorMessages.InvalidToken);
            }

            return ServiceResult.SuccessResult(userId);
        }
    }
}