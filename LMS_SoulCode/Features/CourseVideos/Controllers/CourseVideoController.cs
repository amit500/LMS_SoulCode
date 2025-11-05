using LMS_SoulCode.Features.CourseVideos.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.CourseVideos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseVideoController : ControllerBase
    {
        private readonly CourseVideoService _videoService;

        public CourseVideoController(CourseVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet("list/{courseId}")]
        public async Task<IActionResult> GetVideos(int courseId)
        {
            var videos = await _videoService.GetByCourseAsync(courseId);
            return Ok(videos);
        }
    }
}
