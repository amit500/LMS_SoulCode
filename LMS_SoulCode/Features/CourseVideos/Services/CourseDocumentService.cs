using LMS_SoulCode.Features.Course.Models;
using LMS_SoulCode.Features.CourseVideos.Models;
using LMS_SoulCode.Features.CourseVideos.Repositories;

namespace LMS_SoulCode.Features.CourseVideos.Services
{
    public class CourseDocumentService
    {
        private readonly ICourseDocumentRepository _repository;
        private readonly IWebHostEnvironment _env;

        public CourseDocumentService(ICourseDocumentRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<CourseDocument>> GetByCourseAsync(int courseId)
        {
            return await _repository.GetByCourseIdAsync(courseId);
        }

    }
}
