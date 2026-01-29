using Entities.Models;

namespace Business.Services
{
    public interface IRolService
    {
        Task<ApiResponse<IEnumerable<Roles>>> GetListRoles();
        Task<ApiResponse<Roles>> GetById(int id);
        Task<ApiResponse<int>> CreateRol(RolesSet role);
        Task<ApiResponse<int>> UpdateRol(RolesSet role);
        Task<ApiResponse<int>> DeleteRol(int id);
    }
}
