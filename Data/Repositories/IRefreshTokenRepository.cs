using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<bool> SaveRefreshToken(UserRefreshToken refreshToken);
        Task<UserRefreshToken?> GetRefreshToken(string token); 
    }
}
