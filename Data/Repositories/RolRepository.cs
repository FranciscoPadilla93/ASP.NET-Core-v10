using Entities.Models;
using System.Data;
using Dapper;
using static Data.Context.Context;

namespace Data.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DapperContext _context;
        public RolRepository(DapperContext context) => _context = context;
        public async Task<ApiResponse<IEnumerable<Roles>>> GetListRoles()
        {
            using var connection = _context.CreateConnection();
            var roles = await connection.QueryAsync<Roles>("Stp_Get_RolesList", commandType: CommandType.StoredProcedure);

            if (roles == null || !roles.Any())
                return new ApiResponse<IEnumerable<Roles>>(false, "No se encontraron roles");

            return new ApiResponse<IEnumerable<Roles>>(true, "Roles recuperados", roles);
        }
        public async Task<ApiResponse<Roles>> GetById(int idRol)
        {
            using var conn = _context.CreateConnection();
            var rol = await conn.QueryFirstOrDefaultAsync<Roles>("Stp_Get_RoleByID", new { idRol }, commandType: CommandType.StoredProcedure);

            if (rol == null)
                return new ApiResponse<Roles>(false, "Rol no encontrado.");

            return new ApiResponse<Roles>(true, "Rol recuperado.", rol);
        }

        public async Task<ApiResponse<int>> Create(RolesSet r)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("Name", r.rolName);
            p.Add("Description", r.rolDescription);
            p.Add("userAction", userAction);

            p.Add("idRole", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Stp_Set_RolCreate", p, commandType: CommandType.StoredProcedure);

            return new ApiResponse<int>(
                p.Get<bool>("Success"),
                p.Get<string>("Message"),
                p.Get<int>("idRole")
            );
        }

        public async Task<ApiResponse<int>> Update(RolesSet r)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idRole", r.idRol);
            p.Add("Name", r.rolName);
            p.Add("Description", r.rolDescription);
            p.Add("userAction", userAction);

            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            await conn.ExecuteAsync("Stp_Set_RolUpdate", p, commandType: CommandType.StoredProcedure);

            return new ApiResponse<int>(
                p.Get<bool>("Success"),
                p.Get<string>("Message"),
                p.Get<int>("idRole")
            );
        }

        public async Task<ApiResponse<int>> Delete(int id)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idRole", id);
            p.Add("userAction", userAction);
            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Stp_Set_RolDelete", p, commandType: CommandType.StoredProcedure);

            return new ApiResponse<int>
            (
                p.Get<bool>("Success"),
                p.Get<string>("Message"),
                p.Get<int>("idRole")
            );
        }
    }
}
