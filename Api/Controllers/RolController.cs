using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Entities.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _service;
        public RolController(IRolService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllRoles() => Ok(await _service.GetListRoles());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id) {
            var rol = await _service.GetById(id);
            if (rol == null) 
                return NotFound(new { 
                    message = "Rol no encontrado" }
                );
            return Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolesSet roleRequest)
        {
            if (roleRequest == null)
                return BadRequest("Los datos son obligatorios.");

            var result = await _service.CreateRol(roleRequest);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetRoleById), new { id = result.id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RolesSet roleRequest)
        {
            if (roleRequest == null)
                return BadRequest("Los datos son obligatorios.");

            var result = await _service.UpdateRol(roleRequest);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetRoleById), new { id = result.id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Los datos son obligatorios.");

            var result = await _service.DeleteRol(id);
          
            if (result.Success) 
                return Ok(new { message = result.Message});

            return BadRequest(new { message = result.Message });
        }
    }
}
