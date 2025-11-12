using LMS_SoulCode.Features.SubscribedCourse.Entities;
using LMS_SoulCode.Features.SubscribedCourse.Repositories;

namespace LMS_SoulCode.Features.SubscribedCourse.Services
{
    public interface IUserCourseService
    {
        Task SubscribeAsync(int userId, int courseId);
        Task UnsubscribeAsync(int userId, int courseId);
        Task<bool> IsSubscribedAsync(int userId, int courseId);
        Task<IEnumerable<UserCourse>> GetUserCoursesAsync(int userId);
    }

    public class UserCourseService : IUserCourseService
    {
        private readonly IUserCourseRepository _repo;
        public UserCourseService(IUserCourseRepository repo) => _repo = repo;

        public async Task SubscribeAsync(int userId, int courseId)
        {
            var uc = new UserCourse { UserId = userId, CourseId = courseId, SubscribedAt = DateTime.UtcNow, IsActive = true };
            await _repo.SubscribeAsync(uc);
            await _repo.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(int userId, int courseId)
        {
            var uc = new UserCourse { UserId = userId, CourseId = courseId };
            await _repo.UnsubscribeAsync(uc);
            await _repo.SaveChangesAsync();
        }

        public async Task<bool> IsSubscribedAsync(int userId, int courseId)
            => await _repo.IsSubscribedAsync(userId, courseId);

        public async Task<IEnumerable<UserCourse>> GetUserCoursesAsync(int userId)
            => await _repo.GetByUserAsync(userId);
    }
}
