using Entities.Models;

namespace Business.Services
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<User>>> GetListUsers();
        Task<ApiResponse<User>> GetById(int idUser);
        Task<ApiResponse<int>> CreateUser(UserSet user);
        Task<ApiResponse<int>> UpdateUser(UserSet user);
        Task<ApiResponse<int>> DeleteUser(int idUser);
    }
}
