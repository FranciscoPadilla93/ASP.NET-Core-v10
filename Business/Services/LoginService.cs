using Data.Repositories;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refreshRepo;

        public LoginService(IUserRepository userRepo, IConfiguration config, IRefreshTokenRepository refreshRepo)
        {
            _userRepo = userRepo;
            _config = config;
            _refreshRepo = refreshRepo;
        }

        public async Task<AuthResponse?> Login(string login, string password)
        {
            var response = await _userRepo.GetClienteByEmail(login);

            if (!response.Success || response.Data == null)
                return null;

            var user = response.Data;
            if(!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            var accessToken = GenerarTokenJWT(user, out string jti);
            var refreshToken = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString();

            await _refreshRepo.SaveRefreshToken(new UserRefreshToken
            {
                idUser = user.idUser,
                token = refreshToken,
                jwtId = jti,
                expiryDate = DateTime.UtcNow.AddDays(7) // Dura 7 días
            });

            return new AuthResponse
            {
                Success = true,
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerarTokenJWT(User user, out string jti)
        {
            jti = Guid.NewGuid().ToString();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.idUser.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.userName),
                    new Claim(JwtRegisteredClaimNames.Jti, jti)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthResponse?> RefreshToken(string token)
        {
            var storedToken = await _refreshRepo.GetRefreshToken(token);

            if (storedToken == null || storedToken.isUsed || storedToken.isRevoked || storedToken.expiryDate < DateTime.UtcNow)
            {
                return null;
            }

            var userResponse = await _userRepo.GetById(storedToken.idUser);
            if (!userResponse.Success || userResponse.Data == null) return null;

            var newAccessToken = GenerarTokenJWT(userResponse.Data, out string newJti);
            var newRefreshToken = Guid.NewGuid().ToString();

            await _refreshRepo.SaveRefreshToken(new UserRefreshToken
            {
                idUser = storedToken.idUser,
                token = newRefreshToken,
                jwtId = newJti,
                expiryDate = DateTime.UtcNow.AddDays(7)
            });

            return new AuthResponse
            {
                Success = true,
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
