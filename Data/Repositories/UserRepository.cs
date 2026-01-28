using Entities.Models;
using System.Data;
using Dapper;
using static Data.Context.Context;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<User>> GetListUsers()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<User>("Stp_Get_UsersList", commandType: CommandType.StoredProcedure);
        }
        public async Task<User?> GetById(int idUser)
        {
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>("Stp_Get_UserByID", new { idUser }, commandType: CommandType.StoredProcedure);
        }

        public async Task<User?> GetClienteByEmail(string login)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("Stp_Get_UserByLogin", new { login }, commandType: CommandType.StoredProcedure);
        }

        public async Task<GenericResponse> Create(UserSet r)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idRole", r.idRol);
            p.Add("loginUser", r.loginUser);
            p.Add("PasswordHash", r.PasswordHash);
            p.Add("Email", r.Email);
            p.Add("userName", r.userName);
            p.Add("userAction", userAction);

            p.Add("idUser", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Stp_Set_UserCreate", p, commandType: CommandType.StoredProcedure);

            return new GenericResponse
            {
                Success = p.Get<bool>("Success"),
                Message = p.Get<string>("Message"),
                id = p.Get<int>("idUser")
            };
        }

        public async Task<GenericResponse> Update(UserSet r)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idUser", r.idUser);
            p.Add("idRole", r.idRol);
            p.Add("loginUser", r.loginUser);
            p.Add("PasswordHash", r.PasswordHash);
            p.Add("Email", r.Email);
            p.Add("userName", r.userName);
            p.Add("userAction", userAction);

            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            await conn.ExecuteAsync("Stp_Set_UserUpdate", p, commandType: CommandType.StoredProcedure);

            return new GenericResponse
            {
                Success = p.Get<bool>("Success"),
                Message = p.Get<string>("Message"),
                id = r.idRol
            };
        }

        public async Task<GenericResponse> Delete(int idUser)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idUser", idUser);
            p.Add("userAction", userAction);
            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Stp_Set_UserDelete", p, commandType: CommandType.StoredProcedure);

            return new GenericResponse
            {
                Success = p.Get<bool>("Success"),
                Message = p.Get<string>("Message"),
                id = idUser
            };
        }
    }
}
