using Entities.Models;

namespace Business.Services
{
    public interface ILoginService
    {
        Task<AuthResponse?> Login(string login, string password);
        Task<AuthResponse?> RefreshToken(string token); 
    }
}
