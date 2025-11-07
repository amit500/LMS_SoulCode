using LMS_SoulCode.Features.SubscribedCourse.Entities;

namespace LMS_SoulCode.Features.SubscribedCourse.Repositories
{
    public interface IUserCourseRepository
    {
        Task<UserCourse?> GetAsync(int userId, int courseId);
        Task<IEnumerable<UserCourse>> GetByUserAsync(int userId);
        Task SubscribeAsync(UserCourse userCourse);
        Task UnsubscribeAsync(UserCourse userCourse);
        Task<bool> IsSubscribedAsync(int userId, int courseId);
        Task SaveChangesAsync();
    }
}
