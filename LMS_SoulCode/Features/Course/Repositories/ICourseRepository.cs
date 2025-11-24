using LMS_SoulCode.Features.CourseVideos.Models;
using CourseEntity= LMS_SoulCode.Features.Course.Models.Course;

namespace LMS_SoulCode.Features.Course.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseEntity>> GetAllAsync();
        Task<CourseEntity?> GetByIdAsync(int id);
        Task<List<CourseEntity>> GetCoursesByCateIdAsync(int categoryId);
        Task AddAsync(CourseEntity course);
        Task UpdateAsync(CourseEntity course);
        Task DeleteAsync(CourseEntity course);

        //Course Video Connection
        Task AddVideoAsync(CourseVideo video);
        Task AddDocsAsync(CourseDocument docs);
        Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId);
        Task<IEnumerable<CourseDocument>> GetDocsByCourseIdAsync(int courseId);
    }
}
