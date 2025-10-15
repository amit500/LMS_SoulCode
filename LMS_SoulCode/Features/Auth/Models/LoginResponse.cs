namespace LMS_SoulCode.Features.Auth.Models
{
    public record LoginResponse(string Token, DateTime ExpiresAt);
    public record TokenResponse(string AccessToken, string RefreshToken);
    public record ForgotPasswordResponse(string ResetToken, DateTime TokenExpiresAt);
    public record ResetPasswordResponse(string Message, string AccessToken, string RefreshToken);

}
