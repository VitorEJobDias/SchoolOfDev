using SchoolOfDevs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SchoolOfDevs.Entities;

namespace SchoolOfDevs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _noteService.GetAll());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _noteService.GetById(id));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Note note) => Ok(await _noteService.Create(note));

        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] Note note, int id)
        {
            await _noteService.Update(note, id);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _noteService.Delete(id);
            return Ok();
        }
    }
}
