using LMS_SoulCode.Features.Auth.Entities;
using LMS_SoulCode.Data;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace LMS_SoulCode.Features.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LmsDbContext _context;
        public UserRepository(LmsDbContext context) => _context = context;

        public async Task<User?> GetByUsernameOrEmailAsync(string Email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == Email);
        }
        public async Task<bool> IsEmailTakenAsync(string Email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == Email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GenerateResetTokenAsync(string email)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user == null) return null;
            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // 1 hour expiry
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<bool> ValidateResetTokenAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow);
            return user != null;
        }

        public async Task UpdatePasswordAsync(string email, string newPassword)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.ResetToken = null;
                user.ResetTokenExpiry = null;
                await _context.SaveChangesAsync();
            }
        }

        // Refresh Token 
        public async Task<string> GenerateRefreshTokenAsync(string email)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user == null) return null;
            var token = Guid.NewGuid().ToString();
            user.RefreshToken = token;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7); // 7 days expiry
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);
            return user != null;
        }

        public async Task UpdateRefreshTokenAsync(string email, string newRefreshToken)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user != null)
            {
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                await _context.SaveChangesAsync();
            }
        }
    }

}
