namespace LMS_SoulCode.Features.Auth.DTOs
{
    public record CourseVideoResponse(int Id, int CourseId, string Title, string VideoUrl, DateTime UploadedAt);   

}
