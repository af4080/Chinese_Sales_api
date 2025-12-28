using projectApiAngular.Models;

namespace projectApiAngular.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string email, string username, string phone, Role role);
    }
}