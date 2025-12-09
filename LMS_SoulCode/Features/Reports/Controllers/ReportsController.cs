using LMS_SoulCode.Features.Reports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.Reports.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly CourseReportService _report;

        public ReportsController(CourseReportService report)
        {
            _report = report;
        }

        [HttpGet("course/{userId}/{courseId}")]
        public async Task<IActionResult> GetReport(int userId, int courseId)
        {
            var data = await _report.GetUserCourseReport(userId, courseId);
            return Ok(data);
        }
    }
}
