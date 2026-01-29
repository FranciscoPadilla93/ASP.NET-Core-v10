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
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _service.GetListRoles();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id) {
            var response = await _service.GetById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolesSet roleRequest)
        {
            var result = await _service.CreateRol(roleRequest);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RolesSet roleRequest)
        {
            var result = await _service.UpdateRol(roleRequest);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteRol(id);
          
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
