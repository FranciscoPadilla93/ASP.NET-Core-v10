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

        public async Task<ApiResponse<IEnumerable<User>>> GetListUsers()
        {
            using var connection = _context.CreateConnection();
            var users = await connection.QueryAsync<User>("Stp_Get_UsersList", commandType: CommandType.StoredProcedure);

            if (users == null || !users.Any())
                return new ApiResponse<IEnumerable<User>>(false, "No se encontraron usuarios");

            return new ApiResponse<IEnumerable<User>>(true, "Usuarios recuperados", users);
        }
        public async Task<ApiResponse<User>> GetById(int idUser)
        {
            using var conn = _context.CreateConnection();
            var user = await conn.QueryFirstOrDefaultAsync<User>("Stp_Get_UserByID", new { idUser }, commandType: CommandType.StoredProcedure);

            if (user == null)
                return new ApiResponse<User>(false, "Usuario no encontrado.");

            return new ApiResponse<User>(true, "Usuario recuperado", user);
        }

        public async Task<ApiResponse<User>> GetClienteByEmail(string login)
        {
            using var connection = _context.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>("Stp_Get_UserByLogin", new { login }, commandType: CommandType.StoredProcedure);

            if (user == null)
                return new ApiResponse<User>(false, "Usuario no encontrado.");

            return new ApiResponse<User>(true, "Usuario recuperado", user);
        }

        public async Task<ApiResponse<int>> Create(UserSet r)
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

            return new ApiResponse<int>(
                p.Get<bool>("Success"),
                p.Get<string>("Message"),
                p.Get<int>("idUser")
            );
        }

        public async Task<ApiResponse<int>> Update(UserSet r)
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

            return new ApiResponse<int>(
                p.Get<bool>("Success"),
                p.Get<string>("Message"),
                p.Get<int>("idUser")
            );
        }

        public async Task<ApiResponse<int>> Delete(int idUser)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idUser", idUser);
            p.Add("userAction", userAction);
            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Stp_Set_UserDelete", p, commandType: CommandType.StoredProcedure);

            return new ApiResponse<int>
            (
                p.Get<bool>("Success"),
                p.Get<string>("Message"),
                p.Get<int>("idUser")
            );
        }
    }
}
