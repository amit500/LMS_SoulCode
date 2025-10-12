using Microsoft.AspNetCore.Identity;
using LMS_SoulCode.Features.Auth.Models;
using LMS_SoulCode.Features.Auth.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using LMS_SoulCode.Features.Auth.Entities;
using AuthModel = LMS_SoulCode.Features.Auth.Models.LoginRequest;
using RegisterModel = LMS_SoulCode.Features.Auth.Models.RegisterRequest;

namespace LMS_SoulCode.Features.Auth.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(AuthModel request);
    Task<LoginResponse> RegisterAsync(RegisterModel request);

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
        //if (user == null || user.PasswordHash != request.Password) // plain text
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))

                throw new Exception("Invalid username or password");

        var (token, expires) = _jwt.CreateToken(user);
        return new LoginResponse(token, expires);
    }
    public async Task<LoginResponse> RegisterAsync(RegisterModel request)
    {
        var existing = await _users.GetByUsernameOrEmailAsync(request.Email);
        if (existing != null)
            throw new Exception("User already exists");

        var user = new User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            //PasswordHash=request.Password,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Mobile = request.Mobile,
            Email = request.Email,

        };

        await _users.AddAsync(user);

        var (token, expires) = _jwt.CreateToken(user);
        return new LoginResponse(token, expires);
    }
}
