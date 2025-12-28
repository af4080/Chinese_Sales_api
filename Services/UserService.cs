using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.UserDto;



namespace projectApiAngular.Services
{  
    using BCrypt.Net;
    using Microsoft.OpenApi.Extensions;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        //map user
        private static ReadUserDto MapUser(User u)
        {
            return new ReadUserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                Role = Role.user.GetDisplayName()
            };
        }

        //register user
        public async Task<ReadUserDto> RegisterUser(CreateUserDto user)
        {
            if (await _userRepository.GetUserByEmail(user.Email) != null)
                throw new Exception("User with this email already exists.");
            try
            {
                var NewUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = BCrypt.HashPassword(user.Password),
                    Role = Role.user
                };
                var created = await _userRepository.RegisterUser(NewUser);
                return MapUser(created);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        //login user
        public async Task<string> LoginUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || !BCrypt.Verify(password, user.Password))
            {
                throw new Exception("invalid email or password.");

            }
            var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name, user.Phone, user.Role);


            return token;
        }
    }
}
