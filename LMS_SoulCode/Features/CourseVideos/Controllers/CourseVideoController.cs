using LMS_SoulCode.Features.CourseVideos.Services;
using LMS_SoulCode.Features.SubscribedCourse.Services;
using LMS_SoulCode.Features.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StatusCodes = LMS_SoulCode.Features.Common.StatusCodes;
using LMS_SoulCode.Features.CourseVideos.Models;

namespace LMS_SoulCode.Features.CourseVideos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourseVideoController : ControllerBase
    {
        private readonly CourseVideoService _videoService;
        private readonly IUserCourseService _userCourseService;

        public CourseVideoController(CourseVideoService videoService, IUserCourseService userCourseService)
        {
            _videoService = videoService;
            _userCourseService = userCourseService;
        }

        [HttpGet("list/{courseId}")]
        public async Task<IActionResult> GetVideos(int courseId)
        {
            var videos = await _videoService.GetByCourseAsync(courseId);
            if (videos == null || !videos.Any())
                return StatusCode(StatusCodes.NotFound, ApiResponse<IEnumerable<CourseVideo>>.Fail("No videos found for this course", StatusCodes.NotFound));

            return Ok(ApiResponse<IEnumerable<CourseVideo>>.Success(videos, "Videos retrieved successfully"));
        }

        /*
        [HttpGet("{videoId}/stream")]
        public async Task<IActionResult> Stream(int videoId)
        {
            var video = await _videoService.GetByIdAsync(videoId);
            if (video == null)
                return StatusCode(StatusCodes.NotFound, ApiResponse<string>.Fail("Video not found", StatusCodes.NotFound));

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isSubscribed = await _userCourseService.IsSubscribedAsync(userId, video.CourseId);
            if (!isSubscribed)
                return StatusCode(StatusCodes.Forbidden, ApiResponse<string>.Fail("You must subscribe to this course to view the video.", StatusCodes.Forbidden));

            var url = await _videoService.GetVideoUrlAsync(videoId);
            return Ok(ApiResponse<string>.Success(url, "Video URL retrieved successfully"));
        }
        */
    }
}
