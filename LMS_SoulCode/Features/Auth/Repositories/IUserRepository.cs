using LMS_SoulCode.Features.Auth.Entities;

namespace LMS_SoulCode.Features.Auth.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameOrEmailAsync(string Email);
        Task<bool> IsEmailTakenAsync(string Email);
        Task AddAsync(User user);
        Task<string> GenerateResetTokenAsync(string email);
        Task<bool> ValidateResetTokenAsync(string token);
        Task UpdatePasswordAsync(string email, string newPassword);
        Task<string> GenerateRefreshTokenAsync(string email);
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(string email, string newRefreshToken);
        Task<User?> GetUserByResetTokenAsync(string token);
        Task UpdatePasswordAsync(User user);

    }
}
