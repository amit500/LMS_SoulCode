using CourseEntity = LMS_SoulCode.Features.Course.Entities.Course;
using LMS_SoulCode.Features.Auth.Entities;
namespace LMS_SoulCode.Features.SubscribedCourse.Entities
{
    public class UserCourse
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        // navigation props
        public User User { get; set; } = null!;
        public CourseEntity Course { get; set; } = null!;

       
        // metadata
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
