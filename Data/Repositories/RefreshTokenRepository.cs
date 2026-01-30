using Dapper;
using Data.Context;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using static Data.Context.Context;

namespace Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DapperContext _connection;

        public RefreshTokenRepository(DapperContext connection)
        {
            _connection = connection;
        }

        public async Task<bool> SaveRefreshToken(UserRefreshToken refreshToken)
        {
            using var connection = _connection.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("idUser", refreshToken.idUser);
            parameters.Add("token", refreshToken.token);
            parameters.Add("jwtId", refreshToken.jwtId);
            parameters.Add("expiryDate", refreshToken.expiryDate);

            // Ejecutamos el Stp que creamos antes
            int rows = await connection.ExecuteAsync("stp_SaveRefreshToken",
                parameters,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<UserRefreshToken?> GetRefreshToken(string token)
        {
            using var connection = _connection.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<UserRefreshToken>(
                "SELECT * FROM UserRefreshTokens WHERE token = @token",
                new { token });
        }
    }
}
