using Business.Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService  _service;
        public UserController(IUserService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() => Ok(await _service.GetListUsers());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
                return NotFound(new
                {
                    message = "Usuario no encontrado"
                }
                );
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSet userRequest)
        {
            if (userRequest == null)
                return BadRequest("Los datos son obligatorios.");

            var result = await _service.CreateUser(userRequest);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetUserById), new { id = result.id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserSet userRequest)
        {
            if (userRequest == null)
                return BadRequest("Los datos son obligatorios.");

            var result = await _service.UpdateUser(userRequest);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetUserById), new { id = result.id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Los datos son obligatorios.");

            var result = await _service.DeleteUser(id);

            if (result.Success)


                return Ok(new { message = result.Message });

            return BadRequest(new { message = result.Message });
        }
    }
}
