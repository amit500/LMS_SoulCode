using LMS_SoulCode.Data;
using LMS_SoulCode.Features.UserPermissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly LmsDbContext _context;

        public UserRoleRepository(LmsDbContext context)
        {
            _context = context;
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (!exists)
            {
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };

                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync(); 
            }
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }
    }
}
