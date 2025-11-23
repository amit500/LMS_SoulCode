namespace LMS_SoulCode.Features.Course.DTOs
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Instructor { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public double DurationHours { get; set; }
        public double Rating { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
