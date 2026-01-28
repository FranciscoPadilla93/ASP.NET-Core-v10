using Entities.Models;

namespace Data.Repositories
{
    public interface IRolRepository
    {
        Task<IEnumerable<Roles>> GetListRoles();
        Task<Roles?> GetById(int id);
        Task<GenericResponse> Create(RolesSet role);
        Task<GenericResponse> Update(RolesSet role);
        Task<GenericResponse> Delete(int id);
    }
}
