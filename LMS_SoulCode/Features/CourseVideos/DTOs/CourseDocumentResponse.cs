namespace LMS_SoulCode.Features.Auth.DTOs
{
    public record CourseDocumentResponse(int Id, int CourseId, string DocName, string DocUrl, DateTime UploadedAt);   

}
