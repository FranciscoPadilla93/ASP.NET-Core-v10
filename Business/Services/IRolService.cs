using Entities.Models;

namespace Business.Services
{
    public interface IRolService
    {
        Task<IEnumerable<Roles>> GetListRoles();
        Task<Roles?> GetById(int id);
        Task<GenericResponse> CreateRol(RolesSet role);
        Task<GenericResponse> UpdateRol(RolesSet role);
        Task<GenericResponse> DeleteRol(int id);
    }
}
