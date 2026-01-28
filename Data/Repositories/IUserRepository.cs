using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListUsers();
        Task<User?> GetById(int id);
        Task<User?> GetClienteByEmail(string email);
        Task<GenericResponse> Create(UserSet role);
        Task<GenericResponse> Update(UserSet role);
        Task<GenericResponse> Delete(int id);
    }
}
