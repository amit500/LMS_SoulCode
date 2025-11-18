using LMS_SoulCode.Features.CourseVideos.Services;
using LMS_SoulCode.Features.SubscribedCourse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS_SoulCode.Features.CourseVideos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseVideoController : ControllerBase
    {
        private readonly CourseVideoService _videoService;
        private readonly IUserCourseService _userCourseService; 

        public CourseVideoController(CourseVideoService videoService,IUserCourseService userCourseService)
        {
            _videoService = videoService;
            _userCourseService = userCourseService;

        }

        [HttpGet("list/{courseId}")]
        public async Task<IActionResult> GetVideos(int courseId)
        {
            var videos = await _videoService.GetByCourseAsync(courseId);
            return Ok(videos);
        }

        //[HttpGet("{videoId}/stream")]
        //[Authorize]
        //public async Task<IActionResult> Stream(int videoId)
        //{
        //    var video = await _videoService.GetByIdAsync(videoId);
        //    if (video == null) return NotFound();

        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        //    var isSubscribed = await _userCourseService.IsSubscribedAsync(userId, videoId);
        //    if (!isSubscribed)
        //        return Forbid("You must subscribe to this course to view the video.");

        //    // return video stream / url
        //    var url = await _videoService.GetVideoUrlAsync(videoId);
        //    return Ok(new { videoId, url });
        //}
    }
}
