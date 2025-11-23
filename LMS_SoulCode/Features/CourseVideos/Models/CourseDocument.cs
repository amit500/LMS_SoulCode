using CourseEntity= LMS_SoulCode.Features.Course.Models.Course;
namespace LMS_SoulCode.Features.CourseVideos.Models
{

    public class CourseDocument
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string DocName { get; set; } = string.Empty;
        public string DocUrl { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public CourseEntity Course { get; set; }
    }


}


