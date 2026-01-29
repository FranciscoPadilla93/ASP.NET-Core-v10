using Data.Repositories;
using Entities.Models;

namespace Business.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;
        public RolService(IRolRepository repo) => _repo = repo;
        public Task<ApiResponse<IEnumerable<Roles>>> GetListRoles() => _repo.GetListRoles();
        public Task<ApiResponse<Roles>> GetById(int id) => _repo.GetById(id);

        public async Task<ApiResponse<int>> CreateRol(RolesSet role)
        {
            return await _repo.Create(role);
        }

        public Task<ApiResponse<int>> UpdateRol(RolesSet role) => _repo.Update(role);
        public Task<ApiResponse<int>> DeleteRol(int id) => _repo.Delete(id);
    }
}
