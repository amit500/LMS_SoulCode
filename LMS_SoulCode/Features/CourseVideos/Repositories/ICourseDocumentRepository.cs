using LMS_SoulCode.Features.CourseVideos.Models;

namespace LMS_SoulCode.Features.CourseVideos.Repositories
{
    public interface ICourseDocumentRepository
    {
        Task<IEnumerable<CourseDocument>> GetByCourseIdAsync(int courseId);

    }
}
