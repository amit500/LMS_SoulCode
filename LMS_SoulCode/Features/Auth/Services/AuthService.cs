using LMS_SoulCode.Features.Auth.DTOs;
using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.Auth.Models;
using LMS_SoulCode.Features.Common;
using StatusCodes = LMS_SoulCode.Features.Common.StatusCodes;

namespace LMS_SoulCode.Features.Auth.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ApiResponse<LoginResponse>> RegisterAsync(RegisterRequest request);
        Task<ApiResponse<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ApiResponse<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request);
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

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _users.GetByUsernameOrEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return ApiResponse<LoginResponse>.Fail("Invalid credentials", StatusCodes.Unauthorized);

            var (token, expires) = _jwt.CreateToken(user);

            var dto = new LoginResponse(
                token,
                expires,
                new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }
            );

            return ApiResponse<LoginResponse>.Success(dto, "Login successful");
        }

        public async Task<ApiResponse<LoginResponse>> RegisterAsync(RegisterRequest request)
        {
            if (await _users.IsEmailTakenAsync(request.Email))
                return ApiResponse<LoginResponse>.Fail("Email already exists", StatusCodes.BadRequest);

            var user = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Mobile = request.Mobile,
                Email = request.Email
            };

            await _users.AddAsync(user);

            var (token, expires) = _jwt.CreateToken(user);

            var dto = new LoginResponse(
                token,
                expires,
                new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }
            );

            return ApiResponse<LoginResponse>.Success(dto, "User created successfully");
        }

        public async Task<ApiResponse<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _users.GetByUsernameOrEmailAsync(request.Email);

            if (user == null)
                return ApiResponse<ForgotPasswordResponse>.Fail("Email not found", StatusCodes.NotFound);

            var resetToken = await _users.GenerateResetTokenAsync(request.Email);
            if (resetToken == null)
                return ApiResponse<ForgotPasswordResponse>.Fail("Unable to create reset token", StatusCodes.ServerError);

            var dto = new ForgotPasswordResponse(
                resetToken,
                DateTime.UtcNow.AddHours(1)
            );

            return ApiResponse<ForgotPasswordResponse>.Success(dto, "Reset token generated");
        }

        public async Task<ApiResponse<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _users.GetUserByResetTokenAsync(request.Token);

            if (user == null)
                return ApiResponse<ResetPasswordResponse>.Fail("Invalid or expired token", StatusCodes.BadRequest);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _users.UpdatePasswordAsync(user);

            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;

            var (newAccess, _) = _jwt.CreateToken(user);

            var dto = new ResetPasswordResponse(
                "Password updated successfully",
                newAccess,
                refreshToken
            );

            return ApiResponse<ResetPasswordResponse>.Success(dto, "Password updated");
        }
    }
}
