using LMS_SoulCode.Features.Auth.Models;
using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.Auth.Services;
using LMS_SoulCode.Features.Auth.Entities;
using AuthModel = LMS_SoulCode.Features.Auth.Models.LoginRequest;
using RegisterModel = LMS_SoulCode.Features.Auth.Models.RegisterRequest;
using ForgotPasswordModel = LMS_SoulCode.Features.Auth.Models.ForgotPasswordRequest;
using ResetPasswordModel = LMS_SoulCode.Features.Auth.Models.ResetPasswordRequest;

namespace LMS_SoulCode.Features.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(AuthModel request);
        Task<LoginResponse> RegisterAsync(RegisterModel request);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordModel request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordModel request);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly JwtTokenService _jwt;
        private readonly IEmailService _emailService;
        public AuthService(IUserRepository users, JwtTokenService jwt, IEmailService emailService)
        {
            _users = users;
            _jwt = jwt;
            _emailService = emailService;
        }

        public async Task<LoginResponse> LoginAsync(AuthModel request)
        {
            var user = await _users.GetByUsernameOrEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid username or password");

            var (token, expires) = _jwt.CreateToken(user);
            return new LoginResponse(token, expires);
        }

        public async Task<LoginResponse> RegisterAsync(RegisterModel request)
        {
            if (await _users.IsEmailTakenAsync(request.Email))
                throw new InvalidOperationException("User already exists");

            var user = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Mobile = request.Mobile,
                Email = request.Email,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
            };

            await _users.AddAsync(user);

            var (token, expires) = _jwt.CreateToken(user);
            return new LoginResponse(token, expires);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordModel request)
        {
            var user = await _users.GetByUsernameOrEmailAsync(request.Email);
            if (user == null)
                throw new Exception("User not found");

            var resetToken = await _users.GenerateResetTokenAsync(request.Email);
            if (resetToken == null)
                throw new Exception("Failed to generate reset token");

            var tokenExpiry = DateTime.UtcNow.AddHours(1);

            var resetLink = $"https://example.com/reset-password?email={request.Email}&token={resetToken}";

                    var body = $@"
                Hi {user.UserName},<br><br>
                Click the link below to reset your password:<br>
                <a href='{resetLink}'>Reset Password</a><br><br>
                This link will expire in 1 hour.<br><br>
                Thanks,<br>
                LMS SoulCode Team
            ";

            await _emailService.SendEmailAsync(request.Email, "Reset your password", body);

            return new ForgotPasswordResponse(resetToken, tokenExpiry);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordModel request)
        {
            var user = await _users.GetUserByResetTokenAsync(request.Token);
            if (user == null)
                throw new Exception("Invalid or expired reset token");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            var newRefreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _users.UpdatePasswordAsync(user);

            var (newAccessToken, _) = _jwt.CreateToken(user);

            return new ResetPasswordResponse(
                "Password reset successful. You are now logged in.",
                newAccessToken,
                newRefreshToken
            );
        }
    }
}
