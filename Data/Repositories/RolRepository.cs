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
        public async Task<IEnumerable<Roles>> GetListRoles()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Roles>("Stp_Get_RolesList", commandType: CommandType.StoredProcedure);
        }
        public async Task<Roles?> GetById(int idRol)
        {
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Roles>("Stp_Get_RoleByID", new { idRol }, commandType: CommandType.StoredProcedure);
        }

        public async Task<GenericResponse> Create(RolesSet r)
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

            return new GenericResponse
            {
                Success = p.Get<bool>("Success"),
                Message = p.Get<string>("Message"),
                id = p.Get<int>("idRole")
            };
        }

        public async Task<GenericResponse> Update(RolesSet r)
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

            return new GenericResponse
            {
                Success = p.Get<bool>("Success"),
                Message = p.Get<string>("Message"),
                id = r.idRol
            };
        }

        public async Task<GenericResponse> Delete(int id)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            var userAction = "Test";

            p.Add("idRole", id);
            p.Add("userAction", userAction);
            p.Add("Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Stp_Set_RolDelete", p, commandType: CommandType.StoredProcedure);

            return new GenericResponse
            {
                Success = p.Get<bool>("Success"),
                Message = p.Get<string>("Message"),
                id = id
            };
        }
    }
}
