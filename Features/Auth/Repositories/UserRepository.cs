using LMS_SoulCode.Features.Auth.Entities;
using LMS_SoulCode.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }

}
