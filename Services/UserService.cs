using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.UserDto;



namespace projectApiAngular.Services
{  
    using BCrypt.Net;

    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                Password = u.Password,
                Role = u.Role
            };
        }

        //register user
        public async Task<ReadUserDto> RegisterUser(CreateUserDto user)
        {
             if(await _userRepository.GetUserByEmail(user.Email) != null)
                throw new Exception("User with this email already exists.");
        try
            {
                var NewUser= new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = BCrypt.HashPassword(user.Password),
                    Role = user.Role
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
        public async Task<ReadUserDto?> LoginUser(string email, string password)
        {
            var user= await _userRepository.GetUserByEmail(email);
            if(user == null || !BCrypt.Verify(password , user.Password))
            {
                throw new Exception("invalid email or password.");

            }
            var 

            return MapUser(user);
        }
    }
}
