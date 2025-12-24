using projectApiAngular.Models;
using projectApiAngular.Validations;
using System.ComponentModel.DataAnnotations;

namespace projectApiAngular.DTO
{
    public class UserDto
    {
        public class CreateUserDto
        {
            [Required]
            [MaxLength(50)]
            public required string Name { get; set; }

            [Required]
            [StrongPassword]
            public required string Password { get; set; }

            [EmailAddress]
            [Required]
            [MaxLength(50)]
            public required string Email { get; set; }

            [Phone]
            [Required]
            [MaxLength(15)]
            public required string Phone { get; set; }

            [Required]
            public required Role Role { get; set; }= Role.user;
        }
        public class ReadUserDto
        {

            public int Id { get; set; }
            
            public required string Name { get; set; }

            public required string Password { get; set; }

            [EmailAddress]
            public required string Email { get; set; }

            [Phone]
            public required string Phone { get; set; }

            public required Role Role { get; set; }
        }
    }
}
