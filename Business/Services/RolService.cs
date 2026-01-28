using Data.Repositories;
using Entities.Models;

namespace Business.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;
        public RolService(IRolRepository repo) => _repo = repo;
        public Task<IEnumerable<Roles>> GetListRoles() => _repo.GetListRoles();
        public Task<Roles?> GetById(int id) => _repo.GetById(id);

        public async Task<GenericResponse> CreateRol(RolesSet role)
        {
            return await _repo.Create(role);
        }

        public Task<GenericResponse> UpdateRol(RolesSet role) => _repo.Update(role);
        public Task<GenericResponse> DeleteRol(int id) => _repo.Delete(id);
    }
}
