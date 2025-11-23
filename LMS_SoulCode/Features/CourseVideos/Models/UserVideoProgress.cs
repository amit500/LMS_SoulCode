using LMS_SoulCode.Features.Auth.Models;

namespace LMS_SoulCode.Features.CourseVideos.Models
{
    public class UserVideoProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public double WatchedPercentage { get; set; } 
        public bool IsCompleted { get; set; }
        public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public CourseVideo Video { get; set; } = null!;
    }
}
