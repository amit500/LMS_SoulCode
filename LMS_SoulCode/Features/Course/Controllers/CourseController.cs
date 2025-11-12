using LMS_SoulCode.Features.Course.Models;
using LMS_SoulCode.Features.Course.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.Course.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CourseRequest request)
        {
            await _courseService.AddAsync(request);
            return Ok("Course created successfully");
        }
        //Course Video

        [HttpPost("{courseId}/upload-video")]
        public async Task<IActionResult> UploadVideo(int courseId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected.");

            await _courseService.UploadVideoAsync(courseId, file);
            return Ok("Video uploaded successfully.");
        }

        [HttpGet("{courseId}/videos")]
        public async Task<IActionResult> GetVideos(int courseId)
        {
            var videos = await _courseService.GetCourseVideosAsync(courseId);
            return Ok(videos);
        }
    }
}
