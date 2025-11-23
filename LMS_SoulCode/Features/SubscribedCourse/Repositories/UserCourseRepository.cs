using LMS_SoulCode.Data;
using Microsoft.EntityFrameworkCore;
using LMS_SoulCode.Features.SubscribedCourse.Models;

namespace LMS_SoulCode.Features.SubscribedCourse.Repositories
{
    public class UserCourseRepository : IUserCourseRepository
    {
        private readonly LmsDbContext _context;
        public UserCourseRepository(LmsDbContext context) => _context = context;

        public async Task<UserCourse?> GetAsync(int userId, int courseId)
        {
            return await _context.UserCourses
                .Include(uc => uc.Course)
                .Include(uc => uc.User)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);
        }

        public async Task<IEnumerable<UserCourse>> GetByUserAsync(int userId)
        {
            return await _context.UserCourses
                .Include(uc => uc.Course)
                .Where(x => x.UserId == userId && x.IsActive)
                .ToListAsync();
        }

        public async Task SubscribeAsync(UserCourse userCourse)
        {
            var existing = await GetAsync(userCourse.UserId, userCourse.CourseId);
            if (existing == null)
            {
                await _context.UserCourses.AddAsync(userCourse);
            }
            else
            {
                existing.IsActive = true;
                existing.SubscribedAt = DateTime.UtcNow;
                _context.UserCourses.Update(existing);
            }
        }

        public async Task UnsubscribeAsync(UserCourse userCourse)
        {
            var existing = await GetAsync(userCourse.UserId, userCourse.CourseId);
            if (existing != null)
            {
                existing.IsActive = false;
                _context.UserCourses.Update(existing);
            }
        }

        public async Task<bool> IsSubscribedAsync(int userId, int courseId)
        {
            return await _context.UserCourses.AnyAsync(x => x.UserId == userId && x.CourseId == courseId && x.IsActive);
        }


        public async Task<IEnumerable<object>> GetAllSubscribedAsync()
                     => await _context.UserCourses.Select(x => new {
                         x.UserId,
                         UserName = x.User.UserName,
                         x.CourseId,
                         CourseName = x.Course.Title,
                         x.SubscribedAt,
                         x.IsActive
                     }).ToListAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
