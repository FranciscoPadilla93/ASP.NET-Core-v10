using Azure;
using Business.Services;
using Entities.Models;
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
        public async Task<IActionResult> GetAllUsers() 
        {
            var response = await _service.GetListUsers();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _service.GetById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSet userRequest)
        {
            var result = await _service.CreateUser(userRequest);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserSet userRequest)
        {
            var result = await _service.UpdateUser(userRequest);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUser(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
