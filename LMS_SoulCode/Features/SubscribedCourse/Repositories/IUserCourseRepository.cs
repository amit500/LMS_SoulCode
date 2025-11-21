using LMS_SoulCode.Features.SubscribedCourse.Entities;
using LMS_SoulCode.Features.UserPermissions.Entities;

namespace LMS_SoulCode.Features.SubscribedCourse.Repositories
{
    public interface IUserCourseRepository
    {
        Task<UserCourse?> GetAsync(int userId, int courseId);
        Task<IEnumerable<UserCourse>> GetByUserAsync(int userId);
        Task SubscribeAsync(UserCourse userCourse);
        Task UnsubscribeAsync(UserCourse userCourse);
        Task<bool> IsSubscribedAsync(int userId, int courseId);
        Task<IEnumerable<object>> GetAllSubscribedAsync();

        Task SaveChangesAsync();
    }
}
