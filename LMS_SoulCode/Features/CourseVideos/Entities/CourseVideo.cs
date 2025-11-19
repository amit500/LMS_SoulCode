using CourseEntity= LMS_SoulCode.Features.Course.Entities.Course;
namespace LMS_SoulCode.Features.CourseVideos.Entities
{

    public class CourseVideo
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public CourseEntity Course { get; set; }
    }


}


