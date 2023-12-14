using Application.AuthenticationHandlers.HashManager;
using Application.AuthenticationHandlers.JwtManager;
using Application.Extensions.UserContext;
using Application.UseCases.User;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashManager _hashManager;
        private readonly IJwtManager _jwtManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IHashManager hashManager,
            IJwtManager jwtManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _hashManager = hashManager;
            _jwtManager = jwtManager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            if (users == null)
            {
                throw new Exception("Users not found.");
            }
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<int> Register(User user, string password)
        {
            await _userRepository.Register(user, password);

            return user.Id;
        }

        public async Task<TokenModel> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new Exception("User with this e-mail not found.");
            }
            else if (!_hashManager.VerifyHash(password, user.Hash, user.Salt))
            {
                throw new Exception("The password is incorrect. Please, try again.");
            }
            else
            {
                return new TokenModel
                {
                    Token = _jwtManager.GenerateJwtToken(user),
                    ExpiresAt = DateTimeOffset.UtcNow.AddHours(12)
                };
            }
        }

        //public async Task<List<UserDto>> SearchUsers(string searchText)
        //{
        //    var result = await _userRepository.SearchUsers(searchText);

        //    if (result == null || result.Count <= 0)
        //        throw new InvalidOperationException("Users not found.");

        //    return _mapper.Map<List<UserDto>>(result);
        //}
    }
}
