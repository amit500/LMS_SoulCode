namespace LMS_SoulCode.Features.Auth.Models
{
    public record LoginResponse(string Token, DateTime ExpiresAt);

}
