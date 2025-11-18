using LMS_SoulCode.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.Reports.Services
{
    public class CourseReportService
    {
        private readonly LmsDbContext _context;

        public CourseReportService(LmsDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetUserCourseReport(int userId, int courseId)
        {
            var videos = await _context.CourseVideos
                .Where(v => v.CourseId == courseId)
                .ToListAsync();

            var progress = await _context.UserVideoProgresses
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var totalVideos = videos.Count;
            var completedVideos = progress.Count(p => p.IsCompleted);

            return new
            {
                CourseId = courseId,
                TotalVideos = totalVideos,
                CompletedVideos = completedVideos,
                CompletionPercentage = totalVideos == 0
                    ? 0
                    : (completedVideos * 100.0) / totalVideos
            };
        }
    }
}
