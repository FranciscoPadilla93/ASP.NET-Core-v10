using Data.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;
        public Task<IEnumerable<User>> GetListUsers() => _repo.GetListUsers();
        public Task<User?> GetById(int idUser) => _repo.GetById(idUser);

        public async Task<GenericResponse> CreateUser(UserSet user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            return await _repo.Create(user);
        }

        public Task<GenericResponse> UpdateUser(UserSet user) => _repo.Update(user);
        public Task<GenericResponse> DeleteUser(int idUser) => _repo.Delete(idUser);
    }
}
