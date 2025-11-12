namespace LMS_SoulCode.Features.Auth.Models
{
    public record CourseVideoResponse(int Id, int CourseId, string Title, string VideoUrl, DateTime UploadedAt);   

}
