using Entities.Models;

namespace Data.Repositories
{
    public interface IRolRepository
    {
        Task<ApiResponse<IEnumerable<Roles>>> GetListRoles();
        Task<ApiResponse<Roles>> GetById(int id);
        Task<ApiResponse<int>> Create(RolesSet role);
        Task<ApiResponse<int>> Update(RolesSet role);
        Task<ApiResponse<int>> Delete(int id);
    }
}
