using LMS_SoulCode.Features.Auth.Entities;
using LMS_SoulCode.Features.SubscribedCourse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS_SoulCode.Features.SubscribedCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // must be authenticated
    public class UserCourseController : ControllerBase
    {
        private readonly IUserCourseService _service;
        public UserCourseController(IUserCourseService service) => _service = service;

        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("UserId claim missing");
            return int.Parse(userIdClaim);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest req)
        {
            var userId = GetUserIdFromClaims();
            await _service.SubscribeAsync(userId, req.CourseId);
            return Ok(new { message = "Subscribed successfully", courseId = req.CourseId });
        }

        [HttpPost("unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromBody] SubscribeRequest req)
        {
            var userId = GetUserIdFromClaims();
            await _service.UnsubscribeAsync(userId, req.CourseId);
            return Ok(new { message = "Unsubscribed", courseId = req.CourseId });
        }

        [HttpGet("my-courses")]
        public async Task<IActionResult> MyCourses()
        {
            var userId = GetUserIdFromClaims();
            var courses = await _service.GetUserCoursesAsync(userId);
            var result = courses.Select(c => new
            {
                c.CourseId,
                c.Course.Title,
                c.SubscribedAt,
                c.IsActive
            });
            return Ok(result);
        }

        [HttpGet("Subscribed-List")]
        public async Task<IActionResult> GetAllSubscribed()
        {
            var permissions = await _service.GetAllSubscribedAsync();
            return Ok(permissions);
        }

        [HttpGet("check/{courseId}")]
        public async Task<IActionResult> Check(int courseId)
        {
            var userId = GetUserIdFromClaims();
            var isSubscribed = await _service.IsSubscribedAsync(userId, courseId);
            return Ok(new { courseId, isSubscribed });
        }
    }

    public record SubscribeRequest(int CourseId);
}
