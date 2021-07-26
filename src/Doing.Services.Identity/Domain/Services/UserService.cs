using System.Threading.Tasks;
using Doing.Common.Auth;
using Doing.Common.Exceptions;
using Doing.Services.Identity.Domain.Models;
using Doing.Services.Identity.Domain.Repositories;

namespace Doing.Services.Identity.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IEncrypter _encrypter;

        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository,
                 IEncrypter encrypter,
                 IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LogInAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if(user == null || !user.IsPasswordValid(password, _encrypter)){
                
                throw new DoingException("invalid_credentials",
                    $"Invalid credentials provided"
                );
            }

            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);

            if(user != null){
                throw new DoingException("email_already_used",
                    $"Email '{ email }' is used already"
                );
            }

            user = new User(email, name);

            user.SetPassword(password, _encrypter);

            await _userRepository.AddAsync(user);
        }
    }
}