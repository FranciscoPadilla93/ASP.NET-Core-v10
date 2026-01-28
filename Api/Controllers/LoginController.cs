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
            var token = await _loginService.Login(request.Login, request.Password);
            if (token == null) 
                return Unauthorized(new { message = "Correo o contraseña incorrectos" });

            return Ok(new { token });
        }
    }
}
