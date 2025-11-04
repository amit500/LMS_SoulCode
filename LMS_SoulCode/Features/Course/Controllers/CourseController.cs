using LMS_SoulCode.Features.Course.Models;
using LMS_SoulCode.Features.Course.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.Course.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseRequest request)
        {
            await _courseService.AddAsync(request);
            return Ok("Course created successfully");
        }
    }
}
