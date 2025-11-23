namespace LMS_SoulCode.Features.Auth.DTOs
{
    public record LoginResponse(string Token, DateTime ExpiresAt, UserDto User);
    public record TokenResponse(string AccessToken, string RefreshToken);
    public record ForgotPasswordResponse(string ResetToken, DateTime TokenExpiresAt);
    public record ResetPasswordResponse(string Message, string AccessToken, string RefreshToken);

}
