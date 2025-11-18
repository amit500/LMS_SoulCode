using LMS_SoulCode.Features.CourseVideos.Entities;

namespace LMS_SoulCode.Features.CourseVideos.Repositories
{
    public interface ICourseVideoRepository
    {
        Task<IEnumerable<CourseVideo>> GetByCourseIdAsync(int courseId);

    }
}
