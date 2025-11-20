using LMS_SoulCode.Common;
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
        private readonly PermissionHelper _permissionHelper;
        public CourseController(ICourseService courseService, PermissionHelper permissionHelper)
        {
            _courseService = courseService;
            _permissionHelper= permissionHelper;
        }

        [HttpGet("list")]
        [CheckPermission("CourseList")]

        public async Task<IActionResult> GetAll()
        {           
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CourseRequest request)
        {
            if (!await _permissionHelper.CheckPermission("CreateCourse"))
                return Forbid("Permission Denied");
            await _courseService.AddAsync(request);
            return Ok("Course created successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var courses = await _courseService.GetByIdAsync(id);
            if (courses == null)
                return NotFound(new { message = "course is not found" });

            return Ok(courses);
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
