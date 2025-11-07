using LMS_SoulCode.Features.Course.Entities;
using LMS_SoulCode.Features.CourseVideos.Entities;
using LMS_SoulCode.Features.CourseVideos.Repositories;

namespace LMS_SoulCode.Features.CourseVideos.Services
{
    public class CourseVideoService
    {
        private readonly ICourseVideoRepository _repository;
        private readonly IWebHostEnvironment _env;

        public CourseVideoService(ICourseVideoRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<CourseVideo>> GetByCourseAsync(int courseId)
        {
            return await _repository.GetByCourseIdAsync(courseId);
        }

    }
}
