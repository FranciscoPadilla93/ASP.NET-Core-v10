using Business.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResponse = await _loginService.Login(request.Login, request.Password);

            if (authResponse == null)
                return Unauthorized(new ApiResponse<string>(false, "Correo o contraseña incorrectos"));

            return Ok(new ApiResponse<AuthResponse>(true, "Login exitoso", authResponse));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string token)
        {
            var response = await _loginService.RefreshToken(token);
            if (response == null)
                return Unauthorized(new ApiResponse<string>(false, "Token inválido o expirado"));

            return Ok(new ApiResponse<AuthResponse>(true, "Token renovado", response));
        }
    }
}
