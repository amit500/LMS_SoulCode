using LMS_SoulCode.Features.Auth.Entities;
using LMS_SoulCode.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LmsDbContext _context;

        public UserRepository(LmsDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GenerateResetTokenAsync(string email)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user == null) return null;

            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<bool> ValidateResetTokenAsync(string token)
        {
            return await _context.Users.AnyAsync(
                u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow
            );
        }

        public async Task UpdatePasswordAsync(string email, string newPassword)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user == null) return;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GenerateRefreshTokenAsync(string email)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user == null) return null;

            var token = Guid.NewGuid().ToString();
            user.RefreshToken = token;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user); 
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.AnyAsync(
                u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow
            );
        }

        public async Task UpdateRefreshTokenAsync(string email, string newRefreshToken)
        {
            var user = await GetByUsernameOrEmailAsync(email);
            if (user == null) return;

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user); 
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByResetTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(
                u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow
            );
        }

        public async Task UpdatePasswordAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
