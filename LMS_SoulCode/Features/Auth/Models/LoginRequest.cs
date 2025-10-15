namespace LMS_SoulCode.Features.Auth.Models
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string UserName, string FirstName, string LastName, string Mobile, string Email, string Password);
    public record ForgotPasswordRequest(string Email);
    public record ResetPasswordRequest(string Token, string NewPassword, string ConfirmPassword); 
}
