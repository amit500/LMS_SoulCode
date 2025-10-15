using Microsoft.AspNetCore.Identity;
using LMS_SoulCode.Features.Auth.Models;
using LMS_SoulCode.Features.Auth.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using LMS_SoulCode.Features.Auth.Entities;
using AuthModel = LMS_SoulCode.Features.Auth.Models.LoginRequest;
using RegisterModel = LMS_SoulCode.Features.Auth.Models.RegisterRequest;
using ForgotPasswordModel = LMS_SoulCode.Features.Auth.Models.ForgotPasswordRequest;

namespace LMS_SoulCode.Features.Auth.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(AuthModel request);
    Task<LoginResponse> RegisterAsync(RegisterModel request);
    //Task<LoginResponse> ForgotPasswordAsync(ForgotPasswordModel request);
    //Task<LoginResponse> ResetPasswordAsync(ForgotPasswordModel request);

    //Task<TokenResponse> LoginAsync(AuthModel request);
    //Task<TokenResponse> RefreshTokenAsync(string refreshToken);


}
public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly JwtTokenService _jwt;

    public AuthService(IUserRepository users, JwtTokenService jwt)
    {
        _users = users;
        _jwt = jwt;
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

        };

        await _users.AddAsync(user);

        var (token, expires) = _jwt.CreateToken(user);
        return new LoginResponse(token, expires);
    }
}
