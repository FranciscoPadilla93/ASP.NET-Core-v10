using Entities.Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<ApiResponse<IEnumerable<User>>> GetListUsers();
        Task<ApiResponse<User>> GetById(int id);
        Task<ApiResponse<User>> GetClienteByEmail(string email);
        Task<ApiResponse<int>> Create(UserSet role);
        Task<ApiResponse<int>> Update(UserSet role);
        Task<ApiResponse<int>> Delete(int id);
    }
}
