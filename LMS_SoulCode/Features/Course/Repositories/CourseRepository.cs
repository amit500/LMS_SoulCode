using LMS_SoulCode.Data;
using LMS_SoulCode.Features.Course.Entities;
using LMS_SoulCode.Features.Course.Repositories;
using LMS_SoulCode.Features.CourseVideos.Entities;
using Microsoft.EntityFrameworkCore;
using CourseEntity = LMS_SoulCode.Features.Course.Entities.Course;

namespace LMS_SoulCode.Features.Course.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsDbContext _context;

        public CourseRepository(LmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseEntity>> GetAllAsync() => await _context.Courses.ToListAsync();

        public async Task<CourseEntity?> GetByIdAsync(int id) => await _context.Courses.FindAsync(id);

        public async Task AddAsync(CourseEntity course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseEntity course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CourseEntity course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        //Course Video 
        public async Task AddVideoAsync(CourseVideo video)
        {
             _context.CourseVideos.Add(video);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId)
        {
            return await _context.CourseVideos
                .Where(v => v.CourseId == courseId)
                .ToListAsync();
        }
           
    }
}
