using Data.Repositories;
using Entities.Models;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;
        public Task<ApiResponse<IEnumerable<User>>> GetListUsers() => _repo.GetListUsers();
        public Task<ApiResponse<User>> GetById(int idUser) => _repo.GetById(idUser);

        public async Task<ApiResponse<int>> CreateUser(UserSet user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            return await _repo.Create(user);
        }

        public Task<ApiResponse<int>> UpdateUser(UserSet user) => _repo.Update(user);
        public Task<ApiResponse<int>> DeleteUser(int idUser) => _repo.Delete(idUser);
    }
}
