using LMS_SoulCode.Features.CourseVideos.Models;

namespace LMS_SoulCode.Features.CourseVideos.Repositories
{
    public interface ICourseVideoRepository
    {
        Task<IEnumerable<CourseVideo>> GetByCourseIdAsync(int courseId);

    }
}
