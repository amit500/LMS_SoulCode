using LMS_SoulCode.Features.CourseVideos.Entities;
using CourseEntity= LMS_SoulCode.Features.Course.Entities.Course;

namespace LMS_SoulCode.Features.Course.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseEntity>> GetAllAsync();
        Task<CourseEntity?> GetByIdAsync(int id);
        Task AddAsync(CourseEntity course);
        Task UpdateAsync(CourseEntity course);
        Task DeleteAsync(CourseEntity course);

        //Course Video Connection
        Task AddVideoAsync(CourseVideo video);
        Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId);
    }
}
