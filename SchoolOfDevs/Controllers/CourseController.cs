using SchoolOfDevs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SchoolOfDevs.Entities;

namespace SchoolOfDevs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _courseService.GetAll());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _courseService.GetById(id));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Course course) => Ok(await _courseService.Create(course));

        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] Course course, int id)
        {
            await _courseService.Update(course, id);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.Delete(id);
            return Ok();
        }
    }
}
