using LMS_SoulCode.Data;
using LMS_SoulCode.Features.Course.Repositories;
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
    }
}
