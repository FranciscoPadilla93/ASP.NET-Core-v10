using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetListUsers();
        Task<User?> GetById(int idUser);
        Task<GenericResponse> CreateUser(UserSet user);
        Task<GenericResponse> UpdateUser(UserSet user);
        Task<GenericResponse> DeleteUser(int idUser);
    }
}
