using LMS_SoulCode.Data;
using LMS_SoulCode.Features.Course.Entities;
using LMS_SoulCode.Features.CourseVideos.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.CourseVideos.Repositories
{
    public class CourseVideoRepository : ICourseVideoRepository
    {
        private readonly LmsDbContext _context;

        public CourseVideoRepository(LmsDbContext context)
            =>_context = context;
       

        public async Task<IEnumerable<CourseVideo>> GetByCourseIdAsync(int courseId)
        {
            return await _context.CourseVideos
                .Where(v => v.CourseId == courseId)
                .ToListAsync();
        }
        //public async Task<IEnumerable<CourseVideo>> GetByIdAsync(int videoId)
        //{
        //    return await _context.CourseVideos
        //        .Where(v => v.Id == videoId)
        //        .ToListAsync();
        //}
        //public async Task<IEnumerable<CourseVideo>> GetVideoUrlAsync(string videoId)
        //{
        //    return await _context.CourseVideos
        //        .Where(v => v.VideoUrl == videoId)
        //        .ToListAsync();
        //}
        

    }
}
