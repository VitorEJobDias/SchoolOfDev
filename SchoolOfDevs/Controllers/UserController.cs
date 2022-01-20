using Microsoft.AspNetCore.Mvc;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Services.Interfaces;

namespace SchoolOfDevs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _userService.GetAll());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _userService.GetById(id));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] User user) => Ok(await _userService.Create(user));

        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] User user, int id)
        {
            await _userService.Update(user, id);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
