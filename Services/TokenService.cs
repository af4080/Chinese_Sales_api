using Microsoft.IdentityModel.Tokens;
using projectApiAngular.Configurations;
using projectApiAngular.DTO;
using projectApiAngular.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectApiAngular.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(int userId, string email, string username, string phone, Role role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username ),
                new Claim(ClaimTypes.Role,Role.GetName(typeof(Role),role)!),
                new Claim(ClaimTypes.Email ,email ),
                new Claim("Phone",phone),
                new Claim("Id",userId.ToString())


            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
               signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
