using LMS_SoulCode.Data;
using LMS_SoulCode.Features.Course.Models;
using LMS_SoulCode.Features.CourseVideos.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.CourseVideos.Repositories
{
    public class CourseDocumentRepository : ICourseDocumentRepository
    {
        private readonly LmsDbContext _context;

        public CourseDocumentRepository(LmsDbContext context)
            =>_context = context;
       

        public async Task<IEnumerable<CourseDocument>> GetByCourseIdAsync(int courseId)
        {
            return await _context.CourseDocuments
                .Where(v => v.CourseId == courseId)
                .ToListAsync();
        }
               

    }
}
